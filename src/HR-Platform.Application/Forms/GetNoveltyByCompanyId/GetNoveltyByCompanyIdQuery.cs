using ErrorOr;
using HR_Platform.Application.Forms.Common;
using MediatR;

namespace HR_Platform.Application.Forms.GetNoveltyByCompanyId;

public record GetNoveltyByCompanyIdQuery
(
    Guid CompanyId,

    string EmailWhoIsLogin,

    Guid NoveltyTypeId,

    string CollaboratorName,

    int Page,
    int PageSize
)
:
IRequest<ErrorOr<List<NoveltyByCollaboratorResponse>>>;

