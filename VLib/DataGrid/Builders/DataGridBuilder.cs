using VLib.DataGrid.Builders.MVC;
using VLib.DataGrid.Components;
using VLib.DataGrid.Interfaces;

namespace VLib.DataGrid.Builders;

public class DataGridBuilder : IHtmlContent
{
    private string _id;
    private IEnumerable<object> _dataSource;
    private MvcDataSource _mvcDataSource;
    private List<ColumnComponent> _columns;
    private PagerComponent _pager;
    private EditingComponent _editing;

    public DataGridBuilder()
    {
        _columns = new List<ColumnComponent>();
    }

    public DataGridBuilder ID(string id)
    {
        _id = id;
        return this;
    }

    public DataGridBuilder SetDataSource(IEnumerable<object> dataSource)
    {
        _dataSource = dataSource;
        return this;
    }

    public DataGridBuilder SetDataSource(Action<DataSourceBuilder> dataSourceBuilderAction)
    {
        var dataSourceBuilder = new DataSourceBuilder();
        dataSourceBuilderAction(dataSourceBuilder);
        _mvcDataSource = dataSourceBuilder.Mvc();
        return this;
    }

    public DataGridBuilder AddColumn(Action<ColumnBuilder> columnBuilderAction)
    {
        var columnBuilder = new ColumnBuilder();
        columnBuilderAction(columnBuilder);
        var column = new ColumnComponent(columnBuilder.Header, columnBuilder.Field, columnBuilder.Width, columnBuilder.CellTemplate);
        _columns.Add(column);
        return this;
    }

    public DataGridBuilder Editing(Action<IEditingBuilder> editingBuilderAction)
    {
        var editingBuilder = new EditingBuilder();
        editingBuilderAction(editingBuilder);
        _editing = editingBuilder.Build();
        return this;
    }

    public DataGridBuilder AddPager(Action<PagerBuilder> pagerBuilderAction)
    {
        var pagerBuilder = new PagerBuilder();
        pagerBuilderAction(pagerBuilder);

        _pager = new PagerComponent(
            pagerBuilder.IsVisible,
            pagerBuilder.PageSize,
            pagerBuilder.PageNumber,
            pagerBuilder.TotalItems,
            pagerBuilder.ShowNavigationButtons,
            pagerBuilder.ShowPageSizeSelector,
            pagerBuilder.AllowedPageSizes,
            pagerBuilder.ShowInfo
        );

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