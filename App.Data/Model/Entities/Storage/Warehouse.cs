using App.Data.Model.Entities.Product;

namespace App.Data.Model.Entities.Storage;

public class Warehouse : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public ICollection<StockMovement> StockMovements { get; set; }

}
