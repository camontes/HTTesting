namespace HR_Platform.Application.Surveys.Common;
public record SurveyAndQuestionsResponse
(
    Guid SurveyId,
    Guid CompanyId,

    string Name,
    string Description,

    bool IsVisible,

    List<SurveyQuestionsResponse>? SurveyQuestions
);

public record SurveyQuestionsResponse
(
    Guid SurveyQuestionId,

    string Text,

    int SurveyQuestionTypeId,
    string SurveyQuestionTypeName,
    string SurveyQuestionTypeNameEnglish,

    int SurveyQuestionMandatoryTypeId,
    string SurveyQuestionMandatoryTypeName,
    string SurveyQuestionMandatoryTypeNameEnglish
);
