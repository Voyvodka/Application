namespace App.Data.Dtos.ApiResult;

public class ApiResultModel
{
    public int Result { get; set; }
    public string Err { get; set; }
    public List<ApiErrorModel> Errors { get; set; }
}

