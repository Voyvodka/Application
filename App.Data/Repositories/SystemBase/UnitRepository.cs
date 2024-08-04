using App.Data.Model.SystemEntities;
using App.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories.SystemBase;

public class UnitRepository : GenericRepositoryAsync<Unit>
{
    public UnitRepository(DbContext context) : base(context)
    {
    }
}
