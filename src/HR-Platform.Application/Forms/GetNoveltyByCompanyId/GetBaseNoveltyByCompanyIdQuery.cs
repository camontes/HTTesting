using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Forms.GetNoveltyByCompanyId;

public record GetBaseNoveltyByCompanyIdQuery
(
    Guid NoveltyTypeId,

    string CollaboratorName,

    int Page,
    int PageSize
)
:
IRequest<ErrorOr<bool>>;


