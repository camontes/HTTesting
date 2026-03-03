namespace HR_Platform.Domain.SurveyQuestionMandatoryTypes;

public interface ISurveyQuestionMandatoryTypeRepository
{
    Task<List<SurveyQuestionMandatoryType>> GetAll();
    Task<SurveyQuestionMandatoryType?> GetByIdAsync(SurveyQuestionMandatoryTypeId id);
}
