// using App.Data;
// using App.Data.Model.Entities.Storage;
// using App.Data.Repositories.Storage;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace App.Web.Controllers;

// public class WarehouseController : Controller
// {
//     private AppData _context;
//     private readonly WarehouseRepository _repoWarehouse;
//     public WarehouseController()
//     {
//         _context = new AppData();
//         _repoWarehouse = new WarehouseRepository(_context);
//     }

//     public ActionResult Index() => RedirectToAction("List");

//     public ActionResult List()
//     {
//         return View(_repoWarehouse.GetList()
//                                 .Include(c => c.Client)
//                                 .Include(c => c.Customer)
//                                 .Include(c => c.StockMovements)
//                                 .ToList());
//     }

//     [HttpGet]
//     public async Task<IActionResult> Create(Warehouse warehouse)
//     {
//         if (warehouse?.Id > 0)
//             warehouse = await _repoWarehouse.GetItemAsync(warehouse.Id);
//         warehouse.Clients = [.. _context.Clients.AsNoTracking()];
//         warehouse.Customers = [.. _context.Customers.AsNoTracking()];
//         return View(warehouse);
//     }

//     [HttpPost]
//     public async Task<IActionResult> SaveItem(Warehouse warehouse)
//     {
//         warehouse.ClientId = 1;
//         if (await _repoWarehouse.CreateAsync(warehouse))
//         {
//             return RedirectToAction("EditorShow", new { id = warehouse.Id });
//         }
//         warehouse.Clients = [.. _context.Clients.AsNoTracking()];
//         warehouse.Customers = [.. _context.Customers.AsNoTracking()];
//         return View("Create", warehouse);
//     }

// }
