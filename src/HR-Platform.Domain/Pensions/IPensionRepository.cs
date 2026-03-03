using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.Pensions;

public interface IPensionRepository
{
    Task<List<Pension>> GetAll();
    Task<Pension?> GetByIdAsync(PensionId id);
    Task<Pension?> GetNonePensionByCompanyIdAsync(CompanyId companyId);
    Task<List<Pension>?> GetByCompanyIdAsync(CompanyId CompanyId, int page, int pageSize);
    Task<bool> ExistsAsync(PensionId id);
    Task<int> GetNumberOfPensions(CompanyId id);
    void AddRangePensions(List<Pension> Pension);
    void Add(Pension pension);
    void Update(Pension Pension);
    void Delete(Pension Pension);
    void DeleteRange(List<Pension> Pensions);
}
