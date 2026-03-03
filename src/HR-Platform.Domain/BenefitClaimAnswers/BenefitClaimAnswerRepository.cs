using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.BenefitClaimAnswers;

public interface IBenefitClaimAnswerRepository :  ISearchFilterRepository<BenefitClaimAnswer>
{
    Task<BenefitClaimAnswer?> GetByIdAsync(BenefitClaimAnswerId id);
    Task<List<BenefitClaimAnswer>?> GetByCompanyIdAsync(CompanyId companyId);
    Task<List<BenefitClaimAnswer>> GetBenefitNamesAsync(CompanyId companyId);
    Task<List<BenefitClaimAnswer>> GetByBenefitNameAsync(CompanyId companyId, string benefitName);
    void Add(BenefitClaimAnswer benefitClaimAnswer);
    void DeleteRange(List<BenefitClaimAnswer> benefitClaimAnswers);
    public void UpdateRange(List<BenefitClaimAnswer> benefitClaimAnswers);
}
