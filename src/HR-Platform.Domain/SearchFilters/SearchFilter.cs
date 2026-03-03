namespace HR_Platform.Domain.SearchFilters;

public class SearchFilter<T>
{
    public int TotalCount { get; set; }
    public required IEnumerable<T> Items { get; set; }
}
