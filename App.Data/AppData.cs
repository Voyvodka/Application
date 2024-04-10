using App.Data.Model.SystemEntities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Data;

public class AppData : IdentityDbContext<AppUser, AppRole, string>
{
    public AppData()
    {
    }
    public AppData(DbContextOptions options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if (DEBUG)
        var conn = "Server=localhost;Port=3306;Database=ApplicationDB;User=root;Password=150609%M";
        optionsBuilder.UseMySql(conn, ServerVersion.AutoDetect(conn), b => b.MigrationsAssembly("App.Web"));
#endif
        // #if (!DEBUG)
        //             optionsBuilder.UseMySql(
        //                 Consts.ConnOptions.AppConnection,
        //                 ServerVersion.AutoDetect(Consts.ConnOptions.AppConnection),
        //                 b => b.MigrationsAssembly("App.Web")
        //             );
        // #endif
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }
}