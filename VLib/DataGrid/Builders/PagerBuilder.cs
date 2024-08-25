namespace VLib.DataGrid.Builders;

public class PagerBuilder
{
    public bool IsVisible { get; private set; }
    public int PageSize { get; private set; }
    public int PageNumber { get; private set; }
    public int TotalItems { get; private set; }
    public bool ShowNavigationButtons { get; private set; }
    public bool ShowPageSizeSelector { get; private set; }
    public IEnumerable<int> AllowedPageSizes { get; private set; }
    public bool ShowInfo { get; private set; }

    public PagerBuilder SetPageSize(int pageSize)
    {
        PageSize = pageSize;
        return this;
    }

    public PagerBuilder SetPageNumber(int pageNumber)
    {
        PageNumber = pageNumber;
        return this;
    }

    public PagerBuilder SetTotalItems(int totalItems)
    {
        TotalItems = totalItems;
        return this;
    }

    public PagerBuilder SetShowNavigationButtons(bool show)
    {
        ShowNavigationButtons = show;
        return this;
    }

    public PagerBuilder SetShowPageSizeSelector(bool show)
    {
        ShowPageSizeSelector = show;
        return this;
    }

    public PagerBuilder SetAllowedPageSizes(IEnumerable<int> allowedPageSizes)
    {
        AllowedPageSizes = allowedPageSizes;
        return this;
    }

    public PagerBuilder SetShowInfo(bool show)
    {
        ShowInfo = show;
        return this;
    }

    public PagerBuilder Visible(bool show)
    {
        IsVisible = show;
        return this;
    }
}
