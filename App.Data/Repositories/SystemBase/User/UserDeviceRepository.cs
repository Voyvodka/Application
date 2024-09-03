using App.Data.Model.SystemEntities.User;
using App.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories.SystemBase.User;

public class UserDeviceRepository : GenericRepositoryAsync<UserDevice>
{
    public UserDeviceRepository(DbContext context) : base(context)
    {
    }

    public bool DeviceIsActive(string userId, string deviceId)
    {
        return Conn.UserDevices.Any(x => x.AppUserId == userId && x.Device != deviceId && x.LogoutDate == null);
    }
}
