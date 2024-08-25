namespace VLib.DataGrid.Builders.MVC;
public class MvcDataSource
{
    public string _controllerName { get; private set; }
    public string _loadAction { get; private set; }
    public string _insertAction { get; private set; }
    public string _updateAction { get; private set; }
    public string _deleteAction { get; private set; }
    public string _keyField { get; private set; }

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
