// using App.Data;
// using App.Data.Enums;
// using App.Data.Repositories.Product;
// using AutoMapper;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace App.Web.Controllers;

// public class ListApiController : Controller
// {
//     private readonly AppData _context;
//     private readonly IMapper _mapper;

//     public ListApiController(IMapper mapper)
//     {
//         _context = new AppData();
//         _mapper = mapper;
//     }

//     public async Task<IActionResult> GetStocks(string q)
//     {
//         q ??= "";
//         var _repo = new StockRepository(_context, _mapper);
//         var list = await _repo.GetList(d => d.Name.Contains(q)
//                                         || d.Barcode.Contains(q))
//                 .Include(d => d.Unit)
//                 .OrderBy(d => d.Name)
//                 .Select(d => new { d.Id, text = $"{d.Name} - {d.Barcode} - {d.PackageSizeWithUnit}" })
//                 .Take(25)
//                 .AsNoTracking()
//                 .ToListAsync();
//         return Ok(list);
//     }

//     //TODO type a göre customer gelecek /customer a type eklendikten sonra
//     public async Task<IActionResult> GetCustomers(string q, StockMovementType type)
//     {
//         q ??= "";
//         var _repo = new CustomerRepository(_context, _mapper);
//         var list = await _repo.GetList(d => d.Name.Contains(q))
//                 .OrderBy(d => d.Name)
//                 .Select(d => new { d.Id, text = $"{d.Name}" })
//                 .Take(25)
//                 .AsNoTracking()
//                 .ToListAsync();
//         return Ok(list);
//     }

// }
