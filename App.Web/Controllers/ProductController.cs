using App.Data;
using App.Data.Model.Entities.Product;
using App.Data.Repositories.Product;
using App.Data.ViewModels.Stock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Controllers;

public class ProductController : Controller
{
    private readonly StockRepository _stockRepo;
    private AppData _context;
    public ProductController()
    {
        _context = new AppData();
        _stockRepo = new StockRepository(_context);
    }


    public IActionResult List()
    {
        return View(_stockRepo.GetList().Include(c => c.Unit).ToList());
    }

    [HttpGet]
    public IActionResult Create(StockViewModel stock)
    {
        stock.Units = [.. _context.Units];
        return View(stock);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Stock stock)
    {
        stock.CreatorId = "598c444b-2012-4140-98ef-825d66e401e0";
        stock.ClientId = 1;
        if (await _stockRepo.CreateAsync(stock))
        {
            return View("Show", new { id = stock.Id });
        }
        return View(new StockViewModel()
        {
            Name = stock.Name,
            Barcode = stock.Barcode,
            UnitId = stock.UnitId,
            PackageSize = stock.PackageSize,
            Units = [.. _context.Units]
        });
    }

    // public IActionResult ListView()
    // {
    //     return View();
    // }

    public async Task<List<Stock>> GetStocks()
    {
        return await _stockRepo.GetList().Include(c => c.Unit).ToListAsync();
    }
}
