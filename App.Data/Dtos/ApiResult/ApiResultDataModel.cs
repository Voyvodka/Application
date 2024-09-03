namespace App.Data.Dtos.ApiResult;

public class ApiResultItemModel<T> : ApiResultModel where T : class
{
    public T Item { get; set; }
}

