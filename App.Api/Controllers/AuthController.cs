using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.Data.Repositories.SystemBase;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace App.Api.Controllers;

[ApiController]
[Route("/api/Auth")]
public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly UserLogRepository _repoLog;
    private readonly ICacheService _cacheService;
    private readonly UserDeviceRepository _repoUserDevice;

    public AuthController(AppData context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, ICacheService cacheService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _cacheService = cacheService;

        _repoLog = new UserLogRepository(context);
        _repoUserDevice = new UserDeviceRepository(context);
    }

    [HttpPost]
    [Route("/api/token")]
    [Consumes("application/json")]
    public async Task<IActionResult> GenerateTokenAsync(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
            return Ok(new ApiGenericResultDto(null, -85, "Model Not Valid"));

        var (status, user) = await ValidateUserCredentialsAsync(loginViewModel);
        if (status < 0)
            return Ok(GetErrorResult(status));

        try
        {
            if (!user.IsActive)
                return Ok(new ApiGenericResultDto(null, -55, "LoginErr_CantLogin"));

            var tokenResult = await CreateJwtTokenAsync(user);
            return Ok(new ApiGenericResultDto(tokenResult, 1, null));
        }
        catch (Exception ex)
        {
            return Ok(new ApiGenericResultDto(null, -99, ex.Message));
        }
    }

    private async Task<(int, AppUser)> ValidateUserCredentialsAsync(LoginViewModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null)
            return (-1, null);

        try
        {
            var decodedPassword = model.Password.FromBase64();
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, decodedPassword, false);

            if (signInResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.DeviceId) && _repoUserDevice.DeviceIsActive(user.Id, model.DeviceId))
                    return (-11, null);

                var userRoles = await _userManager.GetRolesAsync(user);
                return (DetermineUserRole(userRoles), user);
            }

            return (-3, null);
        }
        catch
        {
            return (-5, null);
        }
    }

    private static int DetermineUserRole(IEnumerable<string> roles)
    {
        if (roles.Contains("company"))
            return 5;

        if (roles.Contains("nightShift"))
            return 7;

        return 3; // default role
    }

    private async Task<TokenResultModel> CreateJwtTokenAsync(AppUser user)
    {
        var currentTime = DateTime.UtcNow;
        var claims = GenerateUserClaims(user, currentTime);

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = CreateSecurityToken(claims, currentTime);

        await _repoLog.AddLogAsync(user.Id, "MOBILE APP", "0.0.0.0", UserLogTypes.LoginToken);

        var client = _cacheService.GetClient(user.ClientId);
        var tokenResult = new TokenResultModel
        {
            Token = tokenHandler.WriteToken(securityToken),
            Expires = currentTime.AddHours(24),
            ClientName = client?.Name ?? string.Empty,
            ClientLogo = client?.Logo.GetBase64ImagePath() ?? string.Empty,
            Role = DetermineUserRoleLabel(user)
        };

        return tokenResult;
    }

    private List<Claim> GenerateUserClaims(AppUser user, DateTime currentTime)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.AuthTime, currentTime.ToString("dd.MM.yyyy HH:mm:ss")),
            new(JwtRegisteredClaimNames.Nonce, currentTime.Ticks.ToString()),
            new(ClaimTypes.Name, user.UserName)
        };

        var roles = _userManager.GetRolesAsync(user).Result;
        AddRolesToClaims(claims, roles);

        return claims;
    }

    private SecurityToken CreateSecurityToken(List<Claim> claims, DateTime currentTime)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["API:JwtSecret"]));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        return new JwtSecurityTokenHandler().CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = currentTime.AddHours(24),
            NotBefore = currentTime,
            SigningCredentials = signingCredentials
        });
    }

    private string DetermineUserRoleLabel(AppUser user)
    {
        var userRoles = _userManager.GetRolesAsync(user).Result;
        return userRoles.Contains("driver") ? "driver" : "user";
    }

    private static ApiGenericResultDto GetErrorResult(int status)
    {
        return status switch
        {
            -1 => new ApiGenericResultDto(null, -1, "LoginErr_NotFound"),
            -3 => new ApiGenericResultDto(null, -3, "LoginErr_IncorrectPass"),
            -5 => new ApiGenericResultDto(null, -5, "LoginErr_InvalidPass"),
            _ => new ApiGenericResultDto(null, status, "LoginErr_Unexpected")
        };
    }

    private static void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
    {
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
    }


}