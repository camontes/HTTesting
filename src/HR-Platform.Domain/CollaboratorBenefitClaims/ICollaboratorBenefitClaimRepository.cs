using HR_Platform.Domain.Benefits;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.CollaboratorBenefitClaims;
public interface ICollaboratorBenefitClaimRepository : ISearchFilterRepository<CollaboratorBenefitClaim>
{
    Task<CollaboratorBenefitClaim?> GetByIdAsync(CollaboratorBenefitClaimId id);
    Task<CollaboratorBenefitClaim?> ValidateClaimAsync(BenefitId benefitId, CollaboratorId collaboratorId);
    Task<List<CollaboratorBenefitClaim>?> GetByCompanyIdAsync(CompanyId companyId);
    Task<List<CollaboratorBenefitClaim>?> GetByBenefitIdAsync(BenefitId benefitId);
    Task<bool> ExistsAsync(CollaboratorBenefitClaimId id);

    void AddRangeCollaboratorBenefitClaims(List<CollaboratorBenefitClaim> contract);
    void Add(CollaboratorBenefitClaim contract);
    void Update(CollaboratorBenefitClaim contract);
    void Delete(CollaboratorBenefitClaim contract);
}
