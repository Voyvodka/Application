using App.Data.Model.SystemEntities;
using App.Data.Repositories.SystemBase;

namespace App.Api.Controllers.System;

[ApiController]
[Route("/api/unit")]
[Authorize]
public class UnitController : GenericController<Unit, AppData, UnitDto>
{
    private UnitRepository RepoItem => Repo as UnitRepository;

    private readonly ICacheService _cacheService;

    private AppUser ActiveClientUser => _cacheService.GetUser(User.Identity.Name);
    public UnitController(AppData context, IMapper mapper, ICacheService cacheService)
        : base(mapper, context)
    {
        Repo = new UnitRepository(context, mapper);

        _cacheService = cacheService;
    }

    public override void OnCreateSaveItem(Unit item)
    {
        RepoItem.CurrentUserId = ActiveClientUser.Id;
    }
}
