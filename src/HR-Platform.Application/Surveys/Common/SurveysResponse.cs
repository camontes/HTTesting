namespace HR_Platform.Application.Surveys.Common;
public record SurveysResponse
(
    Guid SurveyId,
    Guid CompanyId,

    string Name,
    string Description,

    bool IsVisible,

    string EmailWhoChangedByTH,
    string NameWhoChangedByTH,

    DateTime CreationTime,
    DateTime EditionDate
);

