using VLib.DataGrid.Interfaces;

namespace VLib.DataGrid.Components;

public class ColumnComponent : IColumnComponent
{
    public string Header { get; }
    public string Field { get; }
    public string Width { get; }
    private readonly Func<object, string> _cellTemplate;

    public ColumnComponent(string header, string field, string width, Func<object, string> cellTemplate)
    {
        Header = header;
        Field = field;
        Width = width;
        _cellTemplate = cellTemplate;
    }

    public IHtmlContent RenderHeader()
    {
        var styleAttribute = !string.IsNullOrEmpty(Width) ? $"style='width:{Width}'" : string.Empty;
        return new HtmlString($"<th {styleAttribute}>{Header}</th>");
    }

    public IHtmlContent RenderCell(object value)
    {
        var cellContent = _cellTemplate?.Invoke(value) ?? value?.ToString();
        return new HtmlString($"{cellContent}");
    }

}
