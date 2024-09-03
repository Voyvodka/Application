using App.Data.Model.Entities.General;
using App.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories.General;

public class ClientRepository : GenericRepositoryAsync<Client>
{
    public ClientRepository(DbContext context) : base(context)
    {
    }

    public async Task<List<Client>> GetClientList()
    {
        return await Conn.Clients
            .Where(d => !d.Deleted)
            .ToListAsync();
    }
}
