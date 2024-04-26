using App.Data.Model.SystemEntities;

namespace App.Data.ViewModels.Stock;

public class StockViewModel
{
    public string Name { get; set; }
    public string Barcode { get; set; }

    public decimal PackageSize { get; set; }

    public int UnitId { get; set; }

    public List<Unit> Units { get; set; }
}