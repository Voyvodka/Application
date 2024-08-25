namespace VLib.DataGrid.Builders;

public class RowBuilder
{
    public object DataItem { get; private set; }
    public List<string> Cells { get; private set; }

    public RowBuilder()
    {
        Cells = new List<string>();
    }

    public RowBuilder SetDataItem(object dataItem)
    {
        DataItem = dataItem;
        return this;
    }

    public RowBuilder AddCell(string cellContent)
    {
        Cells.Add(cellContent);
        return this;
    }
}