namespace App.Data.Dtos.ApiResult;

public class ApiResultPagerModel<T> : ApiResultListModel<T> where T : class
{
    public int TotalItemsCount { get; set; }

    public int TotalPageCount { get; set; }

    public int CurrentPageIndex { get; set; }

    public int CurrentPageSize { get; set; }
}
