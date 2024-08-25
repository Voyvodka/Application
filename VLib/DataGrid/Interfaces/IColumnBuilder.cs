using VLib.DataGrid.Enums;

namespace VLib.DataGrid.Interfaces;

public interface IColumnBuilder
{
    IColumnBuilder SetHeader(string header);
    IColumnBuilder SetField(string field);
    IColumnBuilder SetWidth(string width);
    IColumnBuilder SetCellTemplate(Func<object, string> cellTemplate);
}

public interface IColumnComponent
{
    string Header { get; }
    string Field { get; }
    string Width { get; }

    IHtmlContent RenderHeader();
    IHtmlContent RenderCell(object value);

}

public interface ITableComponent
{
    ITableComponent SetId(string id);
    ITableComponent SetColumns(IEnumerable<IColumnComponent> columns);
    ITableComponent SetDataSource(IEnumerable<object> dataSource);
    IHtmlContent Render();
}


public interface IRowComponent
{
    IRowComponent SetColumns(IEnumerable<IColumnComponent> columns);
    IRowComponent SetDataItem(IEnumerable<object> dataItem);
    IHtmlContent Render();
}

public interface IPagerComponent
{
    IPagerComponent Visible(bool show);
    IPagerComponent SetPageSize(int pageSize);
    IPagerComponent SetPageNumber(int pageNumber);
    IPagerComponent SetTotalItems(int totalItems);
    IPagerComponent ShowNavigationButtons(bool show);
    IPagerComponent ShowPageSizeSelector(bool show);
    IPagerComponent SetAllowedPageSizes(IEnumerable<int> allowedPageSizes);
    IPagerComponent ShowInfo(bool show);
    IHtmlContent Render();
}

public interface IEditingBuilder
{
    IEditingBuilder AllowUpdating(bool allowUpdating);
    IEditingBuilder AllowAdding(bool allowAdding);
    IEditingBuilder AllowDeleting(bool allowDeleting);
}
