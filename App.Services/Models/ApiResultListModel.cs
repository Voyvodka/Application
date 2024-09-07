namespace App.Services.Models;

public class ApiResultListModel<T> : ApiResultModel where T : class
{
    public List<T> Items { get; set; }
}
