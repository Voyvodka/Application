namespace App.Api.Controllers.System;

[ApiController]
[Route("/api/warehouse")]
[Authorize]
public class WarehouseController : GenericController<Warehouse, AppData, WarehouseDto>
{
    private WarehouseRepository RepoItem => Repo as WarehouseRepository;

    private readonly ICacheService _cacheService;

    private AppUser ActiveClientUser => _cacheService.GetUser(User.Identity.Name);
    public WarehouseController(AppData context, IMapper mapper, ICacheService cacheService)
        : base(mapper, context)
    {
        Repo = new WarehouseRepository(context, mapper);

        _cacheService = cacheService;
    }

    public override void OnCreateSaveItem(Warehouse item)
    {
        RepoItem.CurrentUserId = ActiveClientUser.Id;
    }

}