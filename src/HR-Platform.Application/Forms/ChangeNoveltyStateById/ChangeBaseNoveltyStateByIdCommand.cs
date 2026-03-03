using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Forms.ChangeNoveltyStateById;

public record ChangeBaseNoveltyStateByIdCommand
(
    Guid FormAnswerGroupId,

    Guid SurveyId,

    int FormAnswerGroupStateId,

    string? DescriptionState,

    List<IFormFile>? Files
)
: IRequest<ErrorOr<bool>>;

