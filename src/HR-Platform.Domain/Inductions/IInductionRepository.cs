using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.Inductions;

public interface IInductionRepository
{
    Task<Induction?> GetByIdAsync(InductionId id);
    Task<Induction?> GetNoneInductionByCompanyIdAsync(CompanyId companyId);
    Task<List<Induction>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<SearchFilter<Induction>> SearchAsync(BasicRequestSearch request);
    Task<bool> ExistsAsync(InductionId id);
    void Add(Induction pension);
    void AddRange(List<Induction> Inductions);
    void Update(Induction Induction);
    void Delete(Induction Induction);
}
