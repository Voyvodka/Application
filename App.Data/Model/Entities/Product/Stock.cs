using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Model.Entities.Base;
using App.Data.Model.Entities.General;
using App.Data.Model.Entities.Storage;
using App.Data.Model.SystemEntities;

namespace App.Data.Model.Entities.Product;

public class Stock : BaseDeleteEntity
{
    [Required]
    [Display(Name = "Stock_Name")]
    public string Name { get; set; }

    [StringLength(50, ErrorMessage = "StringLength")]
    public string Barcode { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal PackageSize { get; set; }

    public int UnitId { get; set; }
    public Unit Unit { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public ICollection<StockMovement> StockMovements { get; set; }
}

public class StockMovement : BaseEntity
{
    public int StockId { get; set; }
    public Stock Stock { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal Price { get; set; }


    public StockMovementType Type { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
}
