using VLib.DataGrid.Components;
using VLib.DataGrid.Interfaces;

namespace VLib.DataGrid.Builders;

public class ColumnBuilder : IColumnBuilder
{
    public string Header { get; private set; }
    public string Field { get; private set; }
    public string Width { get; private set; }
    public Func<object, string> CellTemplate { get; private set; }

    public IColumnBuilder SetHeader(string header)
    {
        Header = header;
        return this;
    }

    public IColumnBuilder SetField(string field)
    {
        Field = field;
        return this;
    }

    public IColumnBuilder SetWidth(string width)
    {
        Width = width;
        return this;
    }

    public IColumnBuilder SetCellTemplate(Func<object, string> cellTemplate)
    {
        CellTemplate = cellTemplate;
        return this;
    }

    public IColumnComponent Build()
    {
        return new ColumnComponent(Header, Field, Width, CellTemplate);
    }
}