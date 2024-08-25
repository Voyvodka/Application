using VLib.DataGrid.Components;
using VLib.DataGrid.Interfaces;

namespace VLib.DataGrid.Builders;

public class TableBuilder : IHtmlContent
{
    private string _id;
    private IEnumerable<IColumnComponent> _columns;
    private IEnumerable<object> _dataSource;
    private PagerComponent _pager;
    private EditingComponent _editing;


    public TableBuilder SetID(string id)
    {
        _id = id;
        return this;
    }

    public TableBuilder SetColumns(IEnumerable<IColumnComponent> columns)
    {
        _columns = columns;
        return this;
    }

    public TableBuilder SetDataSource(IEnumerable<object> dataSource)
    {
        _dataSource = dataSource;
        return this;
    }

    public TableBuilder Paging(PagerComponent pager)
    {
        _pager = pager;
        return this;
    }

    public TableBuilder Editing(EditingComponent editing)
    {
        _editing = editing;
        return this;
    }

    public IHtmlContent Render()
    {
        var tableComponent = new TableComponent(_id, _columns, _dataSource, _pager, _editing);
        return tableComponent.Render();
    }

    public void WriteTo(TextWriter writer, HtmlEncoder encoder)
    {
        writer.Write(Render().ToString());
    }
}
