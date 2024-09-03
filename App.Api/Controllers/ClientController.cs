using App.Data.Model.Entities.General;
using App.Data.Repositories.General;
using Microsoft.AspNetCore.Http;

namespace App.Api.Controllers;

[ApiController]
[Route("/api/Client")]
[Authorize]
public class ClientController : GenericController<Client, AppData, ClientDto>
{
    private ClientRepository RepoItem => Repo as ClientRepository;

    private readonly UserManager<AppUser> _userManager;
    private readonly ICacheService _cacheService;

    private AppUser ActiveClientUser => _cacheService.GetUser(User.Identity.Name);

    public ClientController(AppData context, IMapper mapper, UserManager<AppUser> userManager, ICacheService cacheService)
        : base(mapper, context)
    {
        Repo = new ClientRepository(context);

        _userManager = userManager;
        _cacheService = cacheService;
    }

    public override void OnCreateSaveItem(Client item)
    {
        RepoItem.CurrentUserId = ActiveClientUser.Id;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("update-logo")]
    public async Task<IActionResult> UpdateProfilePicture([FromForm] int clientId, [FromForm] IFormFile logo)
    {
        if (logo == null)
            return BadRequest(ApiGenericResultDto.Fail(null, 0, "Logo is required."));

        var client = await RepoItem.GetItemAsync(clientId);
        if (client == null)
            return NotFound(ApiGenericResultDto.Fail(null, 0, "Client not found."));

        using (var memoryStream = new MemoryStream())
        {
            await logo.CopyToAsync(memoryStream);
            client.Logo = memoryStream.ToArray();
        }

        var result = await RepoItem.EditAsync(client);
        if (!result)
            return StatusCode(500, ApiGenericResultDto.Fail(null, 0, "Logo could not be updated."));

        return Ok(new ApiGenericResultDto(null, 1, "Logo updated successfully."));
    }


}
