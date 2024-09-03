using System.Security.Claims;
using App.Data.Model.Entities.General;
using App.Data.Model.SystemEntities.User;
using App.Data.Repositories.General;
using App.Data.Repositories.SystemBase.User;

namespace App.Data.Services;

public class CacheService : ICacheService
{

    private List<Client> _clientList;
    private List<AppUser> _userList;


    public List<Client> GetClientList()
    {
        if (_clientList == null)
        {
            var repo = new ClientRepository(new AppData());
            _clientList = repo.GetClientList().Result;
        }
        return _clientList;
    }
    public Client GetClient(int id)
    {
        return GetClientList().Where(d => d.Id == id).FirstOrDefault();
    }
    public void AddOrUpdateClient(int id)
    {
        if (_clientList == null)
            return;

        lock (_clientList)
        {
            var repo = new ClientRepository(new AppData());
            var clientItem = repo.GetItemAsync(id).Result;
            var cacheItem = _clientList.FirstOrDefault(d => d.Id == clientItem.Id);
            if (cacheItem == null)
                _clientList.Add(clientItem);
            else
            {
                _clientList.Remove(cacheItem);
                _clientList.Add(clientItem);
            }
        }
    }

    public void RemoveClient(int id)
    {
        if (_clientList == null)
            return;

        lock (_clientList)
        {
            var cacheItem = _clientList.FirstOrDefault(d => d.Id == id);
            if (cacheItem == null)
                _clientList.Remove(cacheItem);
        }
    }


    public List<AppUser> GetUserList()
    {
        if (_userList == null)
        {
            var repo = new AppUserRepository(new AppData());
            _userList = repo.GetUserList().ToList();
        }
        return _userList;
    }

    public AppUser GetUser(string username)
    {
        var list = GetUserList();
        return list.FirstOrDefault(d => d.UserName == username);
    }

    public void AddOrUpdateUserByClientId(int id)
    {
        if (_userList == null)
            return;

        lock (_userList)
        {
            var repo = new AppUserRepository(new AppData());
            var clientItem = repo.GetUserList(id).ToList();

            var cacheItem = _userList.Where(d => d.ClientId == id).ToList();
            if (cacheItem == null)
                _userList.AddRange(clientItem);
            else
            {
                foreach (var item in cacheItem)
                {
                    _userList.Remove(item);
                }
                _userList.AddRange(clientItem);
            }
        }
    }

    //TODO Yanlış
    public void AddOrUpdateAdmin()
    {
        if (_userList == null)
            return;

        lock (_userList)
        {
            var repo = new AppUserRepository(new AppData());
            var clientItem = repo.GetAdminList().ToList();

            var cacheItem = _userList.ToList();
            if (cacheItem == null)
                _userList.AddRange(clientItem);
            else
            {
                foreach (var item in cacheItem)
                {
                    _userList.Remove(item);
                }
                _userList.AddRange(clientItem);
            }
        }
    }

    public void RemoveUserByClientId(int id)
    {
        if (_userList == null)
            return;

        lock (_userList)
        {
            var cacheItem = _userList.Where(d => d.ClientId == id).ToList();
            foreach (var item in cacheItem)
            {
                _userList.Remove(item);
            }
        }
    }

    public AppUser GetUserItem(ClaimsPrincipal user)
    {
        return GetUser(user.Identity.Name);
    }
}