using System.Text;
using VLib.DataGrid.Interfaces;

namespace VLib.DataGrid.Components;

public class PagerComponent : IPagerComponent
{
    private bool _visible;
    private int _pageSize;
    private int _pageNumber;
    private int _totalItems;
    private bool _showNavigationButtons;
    private bool _showPageSizeSelector;
    private IEnumerable<int> _allowedPageSizes;
    private bool _showInfo;

    public PagerComponent(
        bool visible,
        int pageSize,
        int pageNumber,
        int totalItems,
        bool showNavigationButtons,
        bool showPageSizeSelector,
        IEnumerable<int> allowedPageSizes,
        bool showInfo)
    {
        _visible = visible;
        _pageSize = pageSize;
        _pageNumber = pageNumber;
        _totalItems = totalItems;
        _showNavigationButtons = showNavigationButtons;
        _showPageSizeSelector = showPageSizeSelector;
        _allowedPageSizes = allowedPageSizes;
        _showInfo = showInfo;
    }

    public IPagerComponent Visible(bool show)
    {
        _visible = show;
        return this;
    }

    public IPagerComponent SetPageSize(int pageSize)
    {
        _pageSize = pageSize;
        return this;
    }

    public IPagerComponent SetPageNumber(int pageNumber)
    {
        _pageNumber = pageNumber;
        return this;
    }

    public IPagerComponent SetTotalItems(int totalItems)
    {
        _totalItems = totalItems;
        return this;
    }

    public IPagerComponent ShowNavigationButtons(bool show)
    {
        _showNavigationButtons = show;
        return this;
    }

    public IPagerComponent ShowPageSizeSelector(bool show)
    {
        _showPageSizeSelector = show;
        return this;
    }

    public IPagerComponent SetAllowedPageSizes(IEnumerable<int> allowedPageSizes)
    {
        _allowedPageSizes = allowedPageSizes;
        return this;
    }

    public IPagerComponent ShowInfo(bool show)
    {
        _showInfo = show;
        return this;
    }

    public IHtmlContent Render()
    {
        var pagerHtml = new StringBuilder();
        if (_visible)
        {
            var totalPages = (int)Math.Ceiling((double)_totalItems / _pageSize);

            pagerHtml.AppendLine("<div class='d-flex align-items-center'>");

            if (_showPageSizeSelector)
            {
                pagerHtml.AppendLine("<div class='d-flex'>");
                pagerHtml.AppendLine("<select class='form-select form-select-sm form-select-solid' data-control='select2' data-hide-search='true'>");
                foreach (var size in _allowedPageSizes)
                {
                    pagerHtml.AppendLine($"<option value='{size}' {(size == _pageSize ? "selected" : "")}>{size}</option>");
                }
                pagerHtml.AppendLine("</select>");
                pagerHtml.AppendLine("</div>");
            }

            pagerHtml.AppendLine("<ul class='pagination flex-fill'>");

            if (_showNavigationButtons)
            {
                pagerHtml.AppendLine($"<li class='page-item previous {(_pageNumber <= 1 ? "disabled" : "")}'><span class='page-link page-text'>Previous</span></span></li>");
                pagerHtml.AppendLine($"<li class='page-item next {(_pageNumber >= totalPages ? "disabled" : "")}'><a class='page-link page-text' href='#'>Next</span></a></li>");
            }
            pagerHtml.AppendLine("</ul>");



            if (_showInfo)
            {
                pagerHtml.AppendLine($"<div>Page {_pageNumber} of {totalPages}</div>");
            }

            pagerHtml.AppendLine("</div>");
        }
        return new HtmlString(pagerHtml.ToString());
    }
}