using VLib.DataGrid.Builders.MVC;

namespace VLib.DataGrid.Builders;
public class DataSourceBuilder
{
    public MvcDataSource Mvc()
    {
        return new MvcDataSource();
    }
}