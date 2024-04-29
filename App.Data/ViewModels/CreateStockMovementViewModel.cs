using App.Data.Enums;
using App.Data.Model.Entities.Product;
using App.Data.ViewModels.Generic;

namespace App.Data.ViewModels;

public class CreateStockMovementViewModel
{
    public string Description { get; set; }

    public int StockId { get; set; }
    public Stock Stock { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public StockMovementType Type { get; set; }
    public List<SelectListItem> Types { get; set; }

}