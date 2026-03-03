using ErrorOr;
using HR_Platform.Application.Forms.Common;
using MediatR;

namespace HR_Platform.Application.Forms.GetNoveltyByCollaboratorId;

public record GetNoveltyByCollaboratorIdQuery
(
    string EmailWhoIsLogin,

    int WithResponses, // 0 = Both, 1 = Yes, 2 = No

    Guid NoveltyTypeId,

    string CollaboratorName,

    string FormName,

    int Page,
    int PageSize
)
:
IRequest<ErrorOr<List<NoveltyByCollaboratorResponse>>>;

