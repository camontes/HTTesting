using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.TalentPools;

public interface ITalentPoolRepository : ISearchFilterRepository<TalentPool>
{
    Task<List<TalentPool>> GetAll();
    Task<TalentPool?> GetByIdAsync(TalentPoolId id);
    Task<List<TalentPool>?> GetByCompanyIdAsync(CompanyId CompanyId, int page, int pageSize);
    Task<int> GetNumberOfTalentPools(CompanyId id);
    void AddRangeTalentPools(List<TalentPool> TalentPool);
    void Add(TalentPool pension);
    void Update(TalentPool TalentPool);
    void Delete(TalentPool TalentPool);
    void DeleteRange(List<TalentPool> TalentPools);
}
