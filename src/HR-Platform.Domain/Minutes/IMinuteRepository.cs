using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.Minutes;

public interface IMinuteRepository
{
    Task<Minute?> GetByIdAsync(MinuteId id);
    Task<Minute?> GetNoneMinuteByCompanyIdAsync(CompanyId companyId);
    Task<List<Minute>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<bool> ExistsAsync(MinuteId id);
    void Add(Minute pension);
    void AddRange(List<Minute> Minutes);
    void Update(Minute Minute);
    void Delete(Minute Minute);
}
