using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.TalentPools;

namespace HR_Platform.Domain.SearchFilters;

public class FilterByEntities : ICollaboratorFilter, ITalentPoolFilter
{
    public bool? IsPendingInvitation { get; set; }
    public CollaboratorId? CollaboratorId { get; set; }
    public CompanyId? CompanyId { get; set; }
    public AreaId? AreaId { get; set; }
    public bool? IsTalentPoolArchived { get; set; }
    public string? Year { get; set; }
}
