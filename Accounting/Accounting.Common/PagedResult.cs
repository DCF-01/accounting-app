namespace Accounting.Common;

public class PagedResult<T>
{
    public PagedResult(IEnumerable<T> items, int filteredCount)
    {
        Items = items;
        FilteredCount = filteredCount;
    }

    public IEnumerable<T> Items { get; }
    public int TotalCount => Items.Count();
    public int FilteredCount { get; }
}