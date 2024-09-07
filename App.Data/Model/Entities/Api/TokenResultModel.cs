namespace App.Data.Model.Entities.Api;

public class TokenResultModel
{
    public string Token { get; set; }
    public int R { get; set; }
    public string Role { get; set; }
    public DateTime Expires { get; set; }
    public string ClientName { get; set; }
    public string ClientLogo { get; set; }
}
