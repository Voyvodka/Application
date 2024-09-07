namespace App.Data.ViewModels.Dto;

public class WarehouseBaseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
}

public class WarehouseDto : WarehouseBaseDto
{
    public int Id { get; set; }
}