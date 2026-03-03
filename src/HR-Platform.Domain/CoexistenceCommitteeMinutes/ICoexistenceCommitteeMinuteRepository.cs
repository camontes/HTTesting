using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.CoexistenceCommitteeMinutes;

public interface ICoexistenceCommitteeMinuteRepository
{
    Task<CoexistenceCommitteeMinute?> GetByIdAsync(CoexistenceCommitteeMinuteId id);
    Task<CoexistenceCommitteeMinute?> GetNoneCoexistenceCommitteeMinuteByCompanyIdAsync(CompanyId companyId);
    Task<List<CoexistenceCommitteeMinute>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<bool> ExistsAsync(CoexistenceCommitteeMinuteId id);
    void Add(CoexistenceCommitteeMinute pension);
    void AddRange(List<CoexistenceCommitteeMinute> CoexistenceCommitteeMinutes);
    void Update(CoexistenceCommitteeMinute CoexistenceCommitteeMinute);
    void Delete(CoexistenceCommitteeMinute CoexistenceCommitteeMinute);
}
