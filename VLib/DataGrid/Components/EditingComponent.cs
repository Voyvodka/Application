using VLib.DataGrid.Enums;

namespace VLib.DataGrid.Components;

public class EditingComponent
{
    public bool AllowUpdating { get; }
    public bool AllowAdding { get; }
    public bool AllowDeleting { get; }

    public EditingComponent(bool allowUpdating, bool allowAdding, bool allowDeleting)
    {
        AllowUpdating = allowUpdating;
        AllowAdding = allowAdding;
        AllowDeleting = allowDeleting;
    }

    public IHtmlContent Render(EditingRenderPosition edit)
    {

        // Implement rendering logic based on the editing options
        return new HtmlString($"<!-- Editing: Updating={AllowUpdating}, Adding={AllowAdding}, Deleting={AllowDeleting} -->");
    }
}