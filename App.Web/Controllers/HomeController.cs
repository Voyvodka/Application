using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using App.Web.Models;
using App.Data;
using App.Data.Model.Entities.General;
using App.Data.Model.Entities.Product;
using App.Data.Model.SystemEntities.User;
using Microsoft.AspNetCore.Identity;

namespace App.Web.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<HomeController> _logger;

    public HomeController(UserManager<AppUser> userManager, ILogger<HomeController> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        var context = new AppData();

        var client = context.Clients.FirstOrDefault();
        if (client is null)
        {
            client = new Client()
            {
                Name = "Test-" + DateTime.Now.ToString("ss"),
            };
            context.Clients.Add(client);
            context.SaveChanges();
        }
        var user = context.Users.FirstOrDefault();
        if (user is null)
        {
            user = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "samet",
                Email = "smttozkn@gmail.com",
                ClientId = client.Id,
            };
            var stat = await _userManager.CreateAsync(user, "123456");
        }
        context.SaveChanges();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
