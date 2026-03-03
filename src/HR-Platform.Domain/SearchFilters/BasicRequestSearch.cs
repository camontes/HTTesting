namespace HR_Platform.Domain.SearchFilters;

public class BasicRequestSearch : FilterByEntities
{
    public string Query {get; set;} = string.Empty;
    public int Page {get; set;}
    public int PageSize { get; set; }
}
