using App.Data;
using App.Data.Model.SystemEntities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppData>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("AppConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("AppConnection")),
        b => b.MigrationsAssembly("App.Web")
    )
);

builder.Services.AddIdentity<AppUser, AppRole>(config =>
{
    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
    config.Lockout.MaxFailedAccessAttempts = 5;
    config.Password.RequireDigit = false;
    config.Password.RequireUppercase = false;
    config.Password.RequireLowercase = false;
    config.Password.RequiredLength = 6;
    config.Password.RequireNonAlphanumeric = false;
    config.SignIn.RequireConfirmedEmail = false;
    config.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<AppData>()
    .AddDefaultTokenProviders();


builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
            }));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
