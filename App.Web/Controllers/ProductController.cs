using App.Data;
using App.Data.Enums;
using App.Data.Model.Entities.Product;
using App.Data.Repositories.Product;
using App.Data.ViewModels;
using App.Services.Extenders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Controllers;
public class ProductController : Controller
{
    private readonly StockRepository _stockRepo;
    private readonly CustomerRepository _customerRepo;
    private AppData _context;
    public ProductController()
    {
        _context = new AppData();
        _stockRepo = new StockRepository(_context);
        _customerRepo = new CustomerRepository(_context);
    }

    public ActionResult Index() => RedirectToAction("List");

    public IActionResult List()
    {
        return View(_stockRepo.GetList().Include(c => c.Unit).ToList());
    }

    [HttpGet]
    public async Task<IActionResult> Create(Stock stock)
    {
        if (stock?.Id > 0)
            stock = await _stockRepo.GetItemAsync(stock.Id);
        stock.Units = [.. _context.Units.AsNoTracking()];
        return View(stock);
    }

    [HttpPost]
    public async Task<IActionResult> SaveItem(Stock stock)
    {
        stock.CreatorId = _context.Users.FirstOrDefault().Id;
        stock.ClientId = _context.Clients.FirstOrDefault().Id;
        if (await _stockRepo.CreateAsync(stock))
        {
            return RedirectToAction("EditorShow", new { id = stock.Id });
        }
        stock.Units = [.. _context.Units.AsNoTracking()];
        return View("Create", stock);
    }

    public async Task<IActionResult> EditorShow(int id)
    {
        var item = await _stockRepo.GetItemForShow(id);
        return View(item);
    }

    public async Task<IActionResult> StockMovement(CreateStockMovementViewModel model)
    {
        model.Types = StockMovementType.InComing.GetList();
        return View(model);
    }
}
