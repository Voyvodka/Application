using App.Data.Model.SystemEntities.User;
using App.Data.Repositories.Base;
using App.Data.ViewModels.User;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories.SystemBase.User;

public class AppUserRepository : GenericRepositoryAsync<AppUser>
{
    public AppUserRepository(DbContext context) : base(context)
    {
    }

    public async Task<UserAccountViewModel> GetAppUser(string userName)
    {
        return await Conn.AppUsers
            .Where(c => c.UserName == userName)
            .Select(c => new UserAccountViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                UserName = c.UserName,
                Email = c.Email,
                ClientId = c.ClientId,
                ClientName = c.Client.Name
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }


    public IQueryable<AppUser> GetUserList()
    {
        return Conn.AppUsers
            .Include(d => d.Client)
            .AsQueryable();
    }

    public IQueryable<AppUser> GetUserList(int clientId)
    {
        return Conn.AppUsers
            .Include(d => d.Client)
            .Where(d => d.ClientId == clientId && d.IsActive)
            .AsNoTracking();
    }

    //TODO Yanlış
    public IQueryable<AppUser> GetAdminList()
    {
        return Conn.AppUsers.AsQueryable();
    }
}
