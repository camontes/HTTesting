using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.Regulations;

public interface IRegulationRepository : ISearchFilterRepository<Regulation>
{
    Task<Regulation?> GetByIdAsync(RegulationId id);
    Task<Regulation?> GetNoneRegulationByCompanyIdAsync(CompanyId companyId);
    Task<List<Regulation>?> GetByCompanyIdAsync(CompanyId CompanyId, string year);
    Task<bool> ExistsAsync(RegulationId id);
    void Add(Regulation pension);
    void AddRange(List<Regulation> Regulations);
    void Update(Regulation Regulation);
    void Delete(Regulation Regulation);
}
