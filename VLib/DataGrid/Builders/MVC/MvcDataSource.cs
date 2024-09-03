namespace VLib.DataGrid.Builders.MVC;
public class MvcDataSource
{
    private string _controllerName;
    private string _loadAction;
    private string _insertAction;
    private string _updateAction;
    private string _deleteAction;
    private string _keyField;

    public MvcDataSource Controller(string controllerName)
    {
        _controllerName = controllerName;
        return this;
    }

    public MvcDataSource LoadAction(string loadAction)
    {
        _loadAction = loadAction;
        return this;
    }

    public MvcDataSource InsertAction(string insertAction)
    {
        _insertAction = insertAction;
        return this;
    }

    public MvcDataSource UpdateAction(string updateAction)
    {
        _updateAction = updateAction;
        return this;
    }

    public MvcDataSource DeleteAction(string deleteAction)
    {
        _deleteAction = deleteAction;
        return this;
    }

    public MvcDataSource Key(string keyField)
    {
        _keyField = keyField;
        return this;
    }
}
