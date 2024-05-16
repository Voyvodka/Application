using App.Data;
using App.Data.Model.Entities.Product;
using App.Data.Repositories.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Controllers;

public class CustomerController : Controller
{
    private readonly CustomerRepository _customerRepo;
    private readonly StockRepository _stockRepo;
    private AppData _context;
    public CustomerController()
    {
        _context = new AppData();
        _stockRepo = new StockRepository(_context);
        _customerRepo = new CustomerRepository(_context);
    }

    public ActionResult Index() => RedirectToAction("List");

    public IActionResult List()
    {
        return View(_customerRepo.GetList().AsNoTracking().ToList());
    }

    [HttpGet]
    public async Task<IActionResult> Create(Customer customer)
    {
        if (customer.Id > 0)
            customer = await _customerRepo.GetItemAsync(customer.Id);
        return View(customer);
    }

    [HttpPost]
    public async Task<IActionResult> SaveItem(Customer customer)
    {
        if (customer?.Id != 0)
        {
            await _customerRepo.EditAsync(customer);
            return RedirectToAction("EditorShow", new { id = customer.Id });
        }
        if (await _customerRepo.CreateAsync(customer))
        {
            return RedirectToAction("EditorShow", new { id = customer.Id });
        }
        return View("Create", customer);
    }

    public async Task<IActionResult> EditorShow(int id)
    {
        var item = await _customerRepo.GetItemAsync(id);
        return View(item);
    }
}
