namespace Accounting.Common;

public class PagingModel
{
    public string SearchTerm { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int SortColumn { get; }
    public string SortOrder { get; }

    public PagingModel(string searchTerm, int page, int pageSize, int sortColumn, string sortOrder)
    {
        SearchTerm = searchTerm;
        Page = page;
        PageSize = pageSize;
        SortColumn = sortColumn;
        SortOrder = sortOrder;
    }
}