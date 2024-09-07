using App.Data.Model.Entities.Storage;

namespace App.Data.Repositories.Storage;

public class WarehouseRepository : GenericRepositoryAsync<Warehouse>
{
    public WarehouseRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
