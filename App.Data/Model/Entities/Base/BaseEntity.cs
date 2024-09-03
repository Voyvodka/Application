using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Model.SystemEntities.User;

namespace App.Data.Model.Entities.Base;

public class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}

public class BaseCreateEntity : BaseEntity
{
    public DateTime CreatedOn { get; set; }
    public string CreatorId { get; set; }
}

public class BaseDeleteEntity : BaseCreateEntity
{
    public bool Deleted { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string DeletedById { get; set; }
}