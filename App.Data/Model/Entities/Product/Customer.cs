using App.Data.Model.Entities.Base;
using App.Data.Model.Entities.Storage;

namespace App.Data.Model.Entities.Product;

public class Customer : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Warehouse> Warehouses { get; set; }

}