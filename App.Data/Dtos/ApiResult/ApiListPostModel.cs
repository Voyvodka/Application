namespace App.Data.Dtos.ApiResult;

public class ApiListPostModel
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    public string OrderField { get; set; }
    public string OrderDir { get; set; }
    public string Filter { get; set; }


    public void SetOrderInfo(string fieldName, string order = "ASC")
    {
        OrderField = fieldName;
        OrderDir = order;
    }
}

