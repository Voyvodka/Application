using VLib.DataGrid.Extensions;
using VLib.DataGrid.Interfaces;

namespace VLib.DataGrid.Components;

public class RowComponent : IRowComponent
{
    private IEnumerable<IColumnComponent> _columns;
    private IEnumerable<object> _dataItems;

    public RowComponent(IEnumerable<IColumnComponent> columns, IEnumerable<object> dataItems)
    {
        _columns = columns;
        _dataItems = dataItems;
    }

    public IRowComponent SetColumns(IEnumerable<IColumnComponent> columns)
    {
        _columns = columns;
        return this;
    }

    public IRowComponent SetDataItem(IEnumerable<object> dataItems)
    {
        _dataItems = dataItems;
        return this;
    }

    public IHtmlContent Render()
    {
        var rowsHtml = string.Empty;
        int rowIndex = 1;

        foreach (var dataItem in _dataItems)
        {
            int colIndex = 1;
            var rowHtml = $"<tr rowIndex='{rowIndex}'>";
            // if (editing == EditingRenderPosition.RowFirstCell)
            // {
            //     rowHtml += "<td></td>";
            // }
            foreach (var column in _columns)
            {
                var cellValue = GenericEx.GetPropertyValue(dataItem, column.Field);
                var cellHtml = column.RenderCell(cellValue);
                var styleAttribute = !string.IsNullOrEmpty(column.Width) ? $" style='width:{column.Width}'" : string.Empty;

                rowHtml += $"<td{styleAttribute} aria-colindex='{colIndex}'>{cellHtml}</td>";

                colIndex++;
            }
            // if (editing == EditingRenderPosition.RowLastCell)
            // {
            //     rowHtml += "<td></td>";
            // }


            rowHtml += "</tr>";
            rowsHtml += rowHtml;
            rowIndex++;
        }

        return new HtmlString(rowsHtml);
    }
}
