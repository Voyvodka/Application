using App.Data.Model.Entities.Base;
using App.Data.Model.Entities.Storage;
using App.Data.Model.SystemEntities.User;

namespace App.Data.Model.Entities.General;

public class Client : BaseDeleteEntity
{
    public string Name { get; set; }

    public byte[] Logo { get; set; }

    public virtual ICollection<Warehouse> Warehouses { get; set; }
    public virtual ICollection<AppUser> AppUsers { get; set; }
}