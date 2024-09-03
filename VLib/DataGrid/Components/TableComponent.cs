using System.Text;
using VLib.DataGrid.Interfaces;

namespace VLib.DataGrid.Components;
public class TableComponent : ITableComponent
{
    private string _id;
    private IEnumerable<IColumnComponent> _columns;
    private IEnumerable<object> _dataSource;
    private readonly PagerComponent _pager;
    private readonly EditingComponent _editing;

    public TableComponent(string id, IEnumerable<IColumnComponent> columns, IEnumerable<object> dataSource, PagerComponent pager, EditingComponent editing)
    {
        _id = id;
        _columns = columns;
        _dataSource = dataSource;
        _pager = pager;
        _editing = editing;
    }

    public ITableComponent SetId(string id)
    {
        _id = id;
        return this;
    }

    public ITableComponent SetColumns(IEnumerable<IColumnComponent> columns)
    {
        _columns = columns;
        return this;
    }

    public ITableComponent SetDataSource(IEnumerable<object> dataSource)
    {
        _dataSource = dataSource;
        return this;
    }

    public IHtmlContent Render()
    {
        var tableHtml = new StringBuilder();

        tableHtml.Append("<div><div class='table-responsive'>");
        tableHtml.Append($"<table id='{_id}' class='table table-bordered'>");
        tableHtml.Append("<thead><tr>");

        foreach (var column in _columns)
        {
            tableHtml.Append(column.RenderHeader().ToString());
        }

        tableHtml.Append("</tr></thead>");
        tableHtml.Append("<tbody>");

        var rowComponent = new RowComponent(_columns, _dataSource);
        tableHtml.Append(rowComponent.Render().ToString());

        tableHtml.Append("</tbody>");
        tableHtml.Append("</table>");
        tableHtml.Append("</div>");
        tableHtml.Append(_pager.Render().ToString());

        tableHtml.Append("</div>");
        return new HtmlString(tableHtml.ToString());
    }
}