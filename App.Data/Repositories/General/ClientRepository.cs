using App.Data.Model.Entities.General;

namespace App.Data.Repositories.General;

public class ClientRepository : GenericRepositoryAsync<Client>
{
    public ClientRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<Client>> GetClientList()
    {
        return await Conn.Clients
            .Where(d => !d.Deleted)
            .ToListAsync();
    }
}
