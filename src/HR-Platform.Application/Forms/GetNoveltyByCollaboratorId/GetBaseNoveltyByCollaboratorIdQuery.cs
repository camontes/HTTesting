using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Forms.GetNoveltyByCollaboratorId;

public record GetBaseNoveltyByCollaboratorIdQuery
(
    Guid NoveltyTypeId,

    int WithResponses, // 0 = Both, 1 = Yes, 2 = No

    string CollaboratorName,

    string FormName,

    int Page,
    int PageSize
)
:
IRequest<ErrorOr<bool>>;

