using App.Data.Model.Entities.General;
using App.Data.Model.Entities.Product;
using App.Data.Model.Entities.Storage;
using App.Data.Model.SystemEntities;
using App.Data.Model.SystemEntities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Data;

public class AppData : IdentityDbContext<AppUser, AppRole, string>
{
    public string CurrentUserId { get; set; }
    public AppData()
    {
    }
    public AppData(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<UserLog> UserLogs { get; set; }
    public DbSet<UserDevice> UserDevices { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }


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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>()
            .HasMany(c => c.AppUsers)
            .WithOne(u => u.Client)
            .HasForeignKey(u => u.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}