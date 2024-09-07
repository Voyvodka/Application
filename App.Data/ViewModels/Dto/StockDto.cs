namespace App.Data.ViewModels.Dto;

public class StockBaseDto
{
    public string Name { get; set; }
    public string Barcode { get; set; }
    public int Quantity { get; set; }
    public decimal PackageSize { get; set; }
    public int UnitId { get; set; }
}

public class StockDto : StockBaseDto
{
    public int Id { get; set; }
}
