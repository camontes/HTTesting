namespace HR_Platform.Domain.SurveyQuestionTypes;

public interface ISurveyQuestionTypeRepository
{
    Task<List<SurveyQuestionType>> GetAll();
    Task<SurveyQuestionType?> GetByIdAsync(SurveyQuestionTypeId id);
}
