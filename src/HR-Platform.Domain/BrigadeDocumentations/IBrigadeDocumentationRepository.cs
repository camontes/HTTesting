using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.BrigadeDocumentations;

public interface IBrigadeDocumentationRepository : ISearchFilterRepository<BrigadeDocumentation>
{
    Task<BrigadeDocumentation?> GetByIdAsync(BrigadeDocumentationId id);
    Task<BrigadeDocumentation?> GetNoneBrigadeDocumentationByCompanyIdAsync(CompanyId companyId);
    Task<List<BrigadeDocumentation>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<bool> ExistsAsync(BrigadeDocumentationId id);
    void Add(BrigadeDocumentation pension);
    void AddRange(List<BrigadeDocumentation> BrigadeDocumentations);
    void Update(BrigadeDocumentation BrigadeDocumentation);
    void Delete(BrigadeDocumentation BrigadeDocumentation);
}
