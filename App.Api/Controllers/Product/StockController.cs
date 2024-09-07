using App.Data.Repositories.Product;

namespace App.Api.Controllers.Product;

[ApiController]
[Route("/api/stock")]
[Authorize]
public class StockController : GenericController<Stock, AppData, StockDto>
{
    private StockRepository RepoItem => Repo as StockRepository;
    private readonly ICacheService _cacheService;
    private AppUser ActiveClientUser => _cacheService.GetUser(User.Identity.Name);

    public StockController(AppData context, IMapper mapper, ICacheService cacheService)
        : base(mapper, context)
    {
        Repo = new StockRepository(context, mapper);

        _cacheService = cacheService;
    }

    public override void OnCreateSaveItem(Stock item)
    {
        RepoItem.CurrentUserId = ActiveClientUser.Id;
    }
}