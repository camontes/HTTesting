using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Forms.ChangeNoveltyStateById;

public record ChangeNoveltyStateByIdCommand
(
    Guid FormAnswerGroupId,

    Guid SurveyId,

    int FormAnswerGroupStateId,

    string? DescriptionState,

    List<CreateFormAnswerStateFiles> CreateFormAnswerStateFiles
)
:
IRequest<ErrorOr<bool>>;

public record CreateFormAnswerStateFiles(
    string FileName,
    string UrlFile
);
