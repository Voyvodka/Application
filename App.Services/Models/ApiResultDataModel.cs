namespace App.Services.Models;

public class ApiResultItemModel<T> : ApiResultModel where T : class
{
    public T Item { get; set; }
}

