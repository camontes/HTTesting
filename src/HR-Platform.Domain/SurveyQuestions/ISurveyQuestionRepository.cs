using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.SurveyQuestions;

public interface ISurveyQuestionRepository
{
    Task<SurveyQuestion?> GetByIdAsync(SurveyQuestionId id);
    Task<bool> ExistsAsync(SurveyQuestionId id);
    void Add(SurveyQuestion surveyQuestion);
    void AddRange(List<SurveyQuestion> surveyQuestions);
    void Update(SurveyQuestion surveyQuestion);
    void Delete(SurveyQuestion surveyQuestion);
}
