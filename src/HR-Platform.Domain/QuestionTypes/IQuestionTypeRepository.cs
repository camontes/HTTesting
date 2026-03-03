using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.QuestionTypes;

public interface IQuestionTypeRepository
{
    Task<List<QuestionType>> GetAll();
    Task<QuestionType?> GetByIdAsync(QuestionTypeId id);
    Task<QuestionType?> GetNoneQuestionTypeByCompanyIdAsync(CompanyId companyId);
    Task<List<QuestionType>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<bool> ExistsAsync(QuestionTypeId id);
    Task<int> GetNumberOfQuestionTypes(CompanyId id);
    void AddRangeQuestionTypes(List<QuestionType> QuestionType);
    void Add(QuestionType QuestionType);
    void Update(QuestionType QuestionType);
    void Delete(QuestionType QuestionType);
    void DeleteRange(List<QuestionType> QuestionTypes);
}
