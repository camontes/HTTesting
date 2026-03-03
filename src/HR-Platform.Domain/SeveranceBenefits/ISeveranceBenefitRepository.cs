using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.SeveranceBenefits;

public interface ISeveranceBenefitRepository
{
    Task<List<SeveranceBenefit>> GetAll();
    Task<SeveranceBenefit?> GetByIdAsync(SeveranceBenefitId id);
    Task<SeveranceBenefit?> GetNoneSeveranceBenefitByCompanyIdAsync(CompanyId companyId);
    Task<List<SeveranceBenefit>?> GetByCompanyIdAsync(CompanyId CompanyId, int page, int pageSize);
    Task<bool> ExistsAsync(SeveranceBenefitId id);
    Task<int> GetNumberOfSeveranceBenefits(CompanyId id);
    void AddRangeSeveranceBenefits(List<SeveranceBenefit> SeveranceBenefit);
    void Add(SeveranceBenefit pension);

    void Update(SeveranceBenefit SeveranceBenefit);
    void Delete(SeveranceBenefit SeveranceBenefit);
    void DeleteRange(List<SeveranceBenefit> severanceBenefits);
}
