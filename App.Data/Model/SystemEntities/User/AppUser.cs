using App.Data.Model.Entities.General;
using Microsoft.AspNetCore.Identity;

namespace App.Data.Model.SystemEntities.User;

public class AppUser : IdentityUser<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }

    public byte[] ProfilePicture { get; set; }
    public byte[] Banner { get; set; }
    public byte[] Logo { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public virtual ICollection<UserDevice> Devices { get; set; }
}