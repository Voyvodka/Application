using App.Data.Model.SystemEntities.User;
using App.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories.SystemBase;

public class UserLogRepository : GenericRepositoryAsync<UserLog>
{
    public UserLogRepository(DbContext context) : base(context)
    {
    }

    public async Task<bool> AddLogAsync(string userId, string agent, string ip, UserLogTypes ty)
    {
        if (ty == UserLogTypes.SignOut)
        {
            var lastSignIn = await Conn.UserLogs.Where(x => x.AppUserId == userId && x.UserLogType == UserLogTypes.Login).OrderByDescending(x => x.LoginDate).FirstOrDefaultAsync();
            if (lastSignIn != null)
            {
                lastSignIn.LogoutDate = DateTime.Now;
                var diff = lastSignIn.LogoutDate - lastSignIn.LoginDate;
                lastSignIn.DurationSecond = (int)diff.Value.TotalSeconds;
            }
            await Conn.SaveChangesAsync();
            return true;
        }
        var item = new UserLog
        {
            AppUserId = userId,
            Ip = ip,
            LoginDate = DateTime.Now,
            UserAgent = agent,
            UserLogType = ty,
        };
        return await CreateAsync(item);
    }
}