namespace App.Data.ViewModels.User;

public class UserAccountViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    public string Password2 { get; set; }

    public int ClientId { get; set; }
    public string ClientName { get; set; }

    public byte[] ProfilePicture { get; set; }
    public byte[] Banner { get; set; }
    public byte[] Logo { get; set; }

}