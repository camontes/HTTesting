using HR_Platform.Domain.Areas;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.Surveys;

public interface ISurveyRepository
{
    Task<Survey?> GetByIdAsync(SurveyId id);
    Task<List<Survey>?> GetByCompanyIdAsync(CompanyId companyId);
    Task<List<Survey>?> GetByAreaAndCompanyIdAsync(AreaId areaId, CompanyId companyId);
    Task<SearchFilter<Survey>> SearchAsync(BasicRequestSearch request);
    Task<Survey?> GetByIdWithoutIncludesAsync(SurveyId Id);
    Task<bool> ExistsAsync(SurveyId id);
    void Add(Survey survey);
    void AddRange(List<Survey> surveys);
    void Update(Survey survey);
    void Delete(Survey survey);
}
