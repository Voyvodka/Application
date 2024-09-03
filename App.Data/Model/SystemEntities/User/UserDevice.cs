using App.Data.Model.Entities.Base;

namespace App.Data.Model.SystemEntities.User;

public class UserDevice : BaseEntity
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public DateTime LoginDate { get; set; }
    public DateTime? LogoutDate { get; set; }

    public string Token { get; set; }

    public string Device { get; set; }
}