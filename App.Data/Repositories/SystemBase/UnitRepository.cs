using App.Data.Model.SystemEntities;

namespace App.Data.Repositories.SystemBase;

public class UnitRepository : GenericRepositoryAsync<Unit>
{
    public UnitRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
