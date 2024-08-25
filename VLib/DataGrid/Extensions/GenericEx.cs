namespace VLib.DataGrid.Extensions;

public static class GenericEx
{
    public static object GetPropertyValue(object dataItem, string propertyName)
    {
        var property = dataItem.GetType().GetProperty(propertyName);
        return property?.GetValue(dataItem);
    }
}