using App.Data.Model.Entities.Base;
using App.Data.Model.Entities.Storage;

namespace App.Data.Model.Entities.General;

public class Client : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Warehouse> Warehouses { get; set; }
}