using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.ActiveBreaks;

public interface IActiveBreakRepository
{
    Task<ActiveBreak?> GetByIdAsync(ActiveBreakId id);
    Task<List<ActiveBreak>?> GetByCompanyIdAsync(CompanyId companyId);
    Task<bool> ExistsAsync(ActiveBreakId id);
    void Add(ActiveBreak activeBreak);
    void AddRange(List<ActiveBreak> activeBreaks);
    void Update(ActiveBreak activeBreak);
    void Delete(ActiveBreak activeBreak);
}
