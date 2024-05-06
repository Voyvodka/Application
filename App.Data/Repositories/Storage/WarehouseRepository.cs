using App.Data.Model.Entities.Storage;
using App.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories.Storage;

public class WarehouseRepository : GenericRepositoryAsync<Warehouse>
{
    public WarehouseRepository(DbContext context) : base(context)
    {
    }
}
