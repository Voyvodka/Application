using App.Data.Model.SystemEntities.User;

namespace App.Data.Repositories.SystemBase.User;

public class UserDeviceRepository : GenericRepositoryAsync<UserDevice>
{
    public UserDeviceRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public bool DeviceIsActive(string userId, string deviceId)
    {
        return Conn.UserDevices.Any(x => x.AppUserId == userId && x.Device != deviceId && x.LogoutDate == null);
    }
}
