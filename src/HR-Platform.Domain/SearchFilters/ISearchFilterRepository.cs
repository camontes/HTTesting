namespace HR_Platform.Domain.SearchFilters;

public interface ISearchFilterRepository<T> where T : class
{
    Task<SearchFilter<T>> SearchAsync(BasicRequestSearch request);
}
