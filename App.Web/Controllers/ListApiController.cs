using App.Data;
using App.Data.Repositories.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Controllers;

public class ListApiController : Controller
{
    private AppData _context;
    public ListApiController()
    {
        _context = new AppData();
    }
    public async Task<IActionResult> GetStocks(string q)
    {
        q ??= "";
        var _repo = new StockRepository(_context);
        var list = await _repo.GetList(d =>
                                d.Name.ToLower().Contains(q.ToLower()) ||
                                d.Barcode.ToLower().Contains(q.ToLower()))
                .Include(d => d.Unit)
                .OrderBy(d => d.Name)
                .Select(d => new { d.Id, text = $"{d.Name} - {d.Barcode} - {d.PackageSizeWithUnit}" })
                .Take(25)
                .AsNoTracking()
                .ToListAsync();
        return Ok(list);
    }

    public async Task<IActionResult> GetCustomers(string q)
    {
        q ??= "";
        var _repo = new CustomerRepository(_context);
        var list = await _repo.GetList(d => d.Name.ToLower().Contains(q.ToLower()))
                .OrderBy(d => d.Name)
                .Select(d => new { d.Id, text = $"{d.Name}" })
                .Take(25)
                .AsNoTracking()
                .ToListAsync();
        return Ok(list);
    }

}
