using System.Security.Claims;
using App.Data.Model.Entities.General;
using App.Data.Model.SystemEntities.User;

namespace App.Data.Services;

public interface ICacheService
{
    List<Client> GetClientList();
    Client GetClient(int id);
    void AddOrUpdateClient(int id);
    void RemoveClient(int id);

    List<AppUser> GetUserList();
    AppUser GetUser(string username);
    void AddOrUpdateUserByClientId(int id);
    void AddOrUpdateAdmin();
    void RemoveUserByClientId(int id);
    AppUser GetUserItem(ClaimsPrincipal user);

}
