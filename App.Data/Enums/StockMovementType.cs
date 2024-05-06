using System.ComponentModel.DataAnnotations;

namespace App.Data.Enums;
public enum StockMovementType
{
    [Display(Name = "Gelen")]
    InComing = 1,

    [Display(Name = "Giden")]
    OutGoing = 2,
}