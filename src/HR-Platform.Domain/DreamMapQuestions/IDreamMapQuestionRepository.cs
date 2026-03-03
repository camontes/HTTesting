using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.DreamMapQuestions;

public interface IDreamMapQuestionRepository
{
    Task<List<DreamMapQuestion>> GetAll();
    Task<DreamMapQuestion?> GetByIdAsync(DreamMapQuestionId id);
    Task<List<DreamMapQuestion>?> GetByCompanyIdAsync(CompanyId CompanyId);
    void AddRangeDreamMapQuestions(List<DreamMapQuestion> DreamMapQuestion);
    void Add(DreamMapQuestion pension);
    void Update(DreamMapQuestion DreamMapQuestion);
    void UpdateRange(List<DreamMapQuestion> DreamMapQuestions);
    void Delete(DreamMapQuestion DreamMapQuestion);
    void DeleteRange(List<DreamMapQuestion> DreamMapQuestions);
}
