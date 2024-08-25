using VLib.DataGrid.Components;
using VLib.DataGrid.Interfaces;

namespace VLib.DataGrid.Builders;

public class EditingBuilder : IEditingBuilder
{
    private bool _allowUpdating;
    private bool _allowAdding;
    private bool _allowDeleting;

    public IEditingBuilder AllowUpdating(bool allowUpdating)
    {
        _allowUpdating = allowUpdating;
        return this;
    }

    public IEditingBuilder AllowAdding(bool allowAdding)
    {
        _allowAdding = allowAdding;
        return this;
    }

    public IEditingBuilder AllowDeleting(bool allowDeleting)
    {
        _allowDeleting = allowDeleting;
        return this;
    }

    public EditingComponent Build()
    {
        return new EditingComponent(_allowUpdating, _allowAdding, _allowDeleting);
    }
}