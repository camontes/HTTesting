using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.Benefits;

public interface IBenefitRepository
{
    Task<Benefit?> GetByIdAsync(BenefitId id);
    Task<Benefit?> GetNoneBenefitByCompanyIdAsync(CompanyId companyId);
    Task<List<Benefit>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<bool> ExistsAsync(BenefitId id);
    void Add(Benefit pension);
    void AddRange(List<Benefit> Benefits);
    void Update(Benefit Benefit);
    void Delete(Benefit Benefit);
}
