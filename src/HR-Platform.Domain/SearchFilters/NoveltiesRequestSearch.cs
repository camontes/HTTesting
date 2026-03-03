using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;

public class NoveltiesRequestSearch : FilterByEntities
{
    public CompanyId? CompanyId { get; set; }
    public string CollaboratorName { get; set; } = string.Empty;
    public string FormName { get; set; } = string.Empty;

    public int WithResponses; // o = Both, 1 = Yes, 2 = No
    public int Page { get; set; }
    public int PageSize { get; set; }
    public AreaId? AreaId { get; set; }
}
