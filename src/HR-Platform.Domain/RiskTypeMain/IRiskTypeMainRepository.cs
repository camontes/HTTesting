using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.RiskTypeMains;

public interface IRiskTypeMainRepository
{
    Task<List<RiskTypeMain>> GetAll();
    Task<RiskTypeMain?> GetByIdAsync(RiskTypeMainId id);
    Task<RiskTypeMain?> GetNoneRiskTypeMainByCompanyIdAsync(CompanyId companyId);
    Task<List<RiskTypeMain>?> GetByCompanyIdAsync(CompanyId CompanyId);
    void Add(RiskTypeMain pension);
    void Update(RiskTypeMain RiskTypeMain);
    void Delete(RiskTypeMain RiskTypeMain);
}
