using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Model.Entities.Base;
using App.Data.Model.Entities.General;
using App.Data.Model.Entities.Product;

namespace App.Data.Model.Entities.Storage;

public class Warehouse : BaseEntity
{
    public string Name { get; set; }

    public int? ClientId { get; set; }
    public Client Client { get; set; }

    public int? CustomerId { get; set; }
    public Customer Customer { get; set; }

    public ICollection<StockMovement> StockMovements { get; set; }

    [NotMapped]
    public List<Client> Clients { get; set; }
    [NotMapped]
    public List<Customer> Customers { get; set; }
}
