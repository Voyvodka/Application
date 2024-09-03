namespace App.Data.Model.Entities.Api;

public class ApiGenericResultDto
{
    public ApiGenericResultDto(object _data, int _result, string _err)
    {
        Data = _data;
        Result = _result;
        Err = _err;
    }
    public object Data { get; set; }

    public int Result { get; set; }

    public string Err { get; set; }


    public static ApiGenericResultDto Fail(object data, int result, string error)
    {
        return new ApiGenericResultDto(data, result, error);
    }
}