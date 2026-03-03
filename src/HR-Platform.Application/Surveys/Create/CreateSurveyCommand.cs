using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Surveys.Create;

public record CreateSurveyCommand
(
    Guid CompanyId,

    Guid SurveyTypeId,

    string Name,

    string Description,

    string EmailWhoChangedByTH,
    string NameWhoChangedByTH,

    bool IsVisible,

    List<CreateBaseSurveyQuestionCommand> SurveyQuestions
)
:
IRequest<ErrorOr<bool>>;


