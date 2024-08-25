using Microsoft.AspNetCore.Mvc.Rendering;
using VLib.DataGrid.Builders;

namespace VLib;

public static class HtmlHelperExtension
{
    public static VLibHelper VLib(this IHtmlHelper htmlHelper)
    {
        return new VLibHelper(htmlHelper);
    }
}

public class VLibHelper
{
    private readonly IHtmlHelper _htmlHelper;

    public VLibHelper(IHtmlHelper htmlHelper)
    {
        _htmlHelper = htmlHelper;
    }

    public DataGridBuilder DataGrid()
    {
        return new DataGridBuilder();
    }
}