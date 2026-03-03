using HR_Platform.Domain.SearchFilters;

public class ImprovementPlansRequestSearch : FilterByEntities
{
    public Guid EvaluatorId { get; set; }

    public int WithResponses; // 0 = Both, 1 = Yes, 2 = No
    public string CollaboratorName { get; set; } = string.Empty;
    public int Page { get; set; }
    public int PageSize { get; set; }
}
