namespace App.Data.Dtos.ApiResult;

public class ApiResultListModel<T> : ApiResultModel where T : class
{
    public List<T> Items { get; set; }
}
