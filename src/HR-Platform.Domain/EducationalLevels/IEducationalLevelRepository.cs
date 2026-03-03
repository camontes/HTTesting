using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.EducationalLevels;

public interface IEducationalLevelRepository
{
    Task<List<EducationalLevel>> GetAll();
    Task<EducationalLevel?> GetByIdAsync(EducationalLevelId id);
    Task<EducationalLevel?> GetNoneEducationalLevelByCompanyIdAsync(CompanyId companyId);
    Task<List<EducationalLevel>?> GetByCompanyIdAsync(CompanyId CompanyId, int page, int pageSize);
    Task<bool> ExistsAsync(EducationalLevelId id);
    Task<int> GetNumberOfEducationalLevels(CompanyId id);
    void AddRangeEducationalLevels(List<EducationalLevel> EducationalLevel);
    void Add(EducationalLevel pension);
    void Update(EducationalLevel EducationalLevel);
    void Delete(EducationalLevel EducationalLevel);
    void DeleteRange(List<EducationalLevel> EducationalLevels);
}
