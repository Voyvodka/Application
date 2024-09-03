using Microsoft.AspNetCore.Http;

namespace App.Api.Controllers;

[Route("/api/Account")]
public class AccountController : ApiBaseController
{
    private readonly AppUserRepository _repoUser;
    private readonly UserManager<AppUser> _userManager;
    private readonly ICacheService _cacheService;

    private AppUser ActiveClientUser => _cacheService.GetUser(User.Identity.Name);

    public AccountController(AppData context, UserManager<AppUser> userManager, ICacheService cacheService)
    {
        _userManager = userManager;
        _cacheService = cacheService;

        _repoUser = new AppUserRepository(context);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetAccount()
    {
        var userName = User.Identity?.Name;

        if (userName.IsEmpty())
            return NotFound(new ApiGenericResultDto(null, 404, "User is not authenticated."));

        var userAccount = await _repoUser.GetAppUser(userName);

        if (userAccount == null)
            return NotFound(new ApiGenericResultDto(null, 404, $"User with username '{userName}' not found."));

        var result = new ApiGenericResultDto(userAccount, 200, null);
        return Ok(result);
    }

    [HttpPut("update-info")]
    public async Task<IActionResult> UpdateUserInfo([FromBody] UserAccountViewModel model)
    {
        if (model == null)
            return BadRequest(ApiGenericResultDto.Fail(null, 0, "Invalid user data."));

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(ApiGenericResultDto.Fail(null, 0, "User not found."));

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.PhoneNumber = model.PhoneNumber;

        if (!model.Email.IsEmpty())
        {
            if (!model.Email.IsValidEmail())
                return BadRequest(ApiGenericResultDto.Fail(null, 0, "Invalid email address."));

            var emailInUse = await _userManager.FindByEmailAsync(model.Email);
            if (emailInUse != null && emailInUse.Id != user.Id)
                return BadRequest(ApiGenericResultDto.Fail(null, 0, "Email address is already in use."));

            user.Email = model.Email;
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return StatusCode(500, ApiGenericResultDto.Fail(null, 0, "User information could not be updated."));

        return Ok(new ApiGenericResultDto(model, 1, "User information updated successfully."));
    }

    [HttpPut("update-password")]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UserAccountViewModel model)
    {
        if (model == null)
            return BadRequest(ApiGenericResultDto.Fail(null, 0, "Invalid user data."));

        if (model.Password != model.Password2)
            return BadRequest(ApiGenericResultDto.Fail(null, 0, "Passwords do not match."));

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(ApiGenericResultDto.Fail(null, 0, "User not found."));

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

        if (!result.Succeeded)
            return StatusCode(500, ApiGenericResultDto.Fail(null, 0, "Password could not be updated."));

        return Ok(new ApiGenericResultDto(null, 1, "Password updated successfully."));
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("update-profile-picture")]
    public async Task<IActionResult> UpdateProfilePicture([FromForm] IFormFile profilePicture)
    {
        if (profilePicture == null)
            return BadRequest(ApiGenericResultDto.Fail(null, 0, "Profile picture is required."));

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(ApiGenericResultDto.Fail(null, 0, "User not found."));

        using (var memoryStream = new MemoryStream())
        {
            await profilePicture.CopyToAsync(memoryStream);
            user.ProfilePicture = memoryStream.ToArray();
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return StatusCode(500, ApiGenericResultDto.Fail(null, 0, "Profile picture could not be updated."));

        return Ok(new ApiGenericResultDto(null, 1, "Profile picture updated successfully."));
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("update-banner")]
    public async Task<IActionResult> UpdateBanner([FromForm] IFormFile banner)
    {
        if (banner == null)
            return BadRequest(ApiGenericResultDto.Fail(null, 0, "Banner image is required."));

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(ApiGenericResultDto.Fail(null, 0, "User not found."));

        using (var memoryStream = new MemoryStream())
        {
            await banner.CopyToAsync(memoryStream);
            user.Banner = memoryStream.ToArray();
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return StatusCode(500, ApiGenericResultDto.Fail(null, 0, "Banner could not be updated."));

        return Ok(new ApiGenericResultDto(null, 1, "Banner updated successfully."));
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("update-logo")]
    public async Task<IActionResult> UpdateLogo([FromForm] IFormFile logo)
    {
        if (logo == null)
            return BadRequest(ApiGenericResultDto.Fail(null, 0, "Logo image is required."));

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return NotFound(ApiGenericResultDto.Fail(null, 0, "User not found."));

        using (var memoryStream = new MemoryStream())
        {
            await logo.CopyToAsync(memoryStream);
            user.Logo = memoryStream.ToArray();
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return StatusCode(500, ApiGenericResultDto.Fail(null, 0, "Logo could not be updated."));

        return Ok(new ApiGenericResultDto(null, 1, "Logo updated successfully."));
    }

}