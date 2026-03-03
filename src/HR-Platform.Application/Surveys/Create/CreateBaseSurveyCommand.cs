using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Surveys.Create;

public record CreateBaseSurveyCommand
(
    Guid SurveyTypeId,

    string Name,

    string Description,

    bool IsVisible,

    List<CreateBaseSurveyQuestionCommand> SurveyQuestions
)
:
IRequest<ErrorOr<bool>>;

public record CreateBaseSurveyQuestionCommand
(
    string Text,

    int SurveyQuestionTypeId,

    int SurveyQuestionMandatoryTypeId
);


