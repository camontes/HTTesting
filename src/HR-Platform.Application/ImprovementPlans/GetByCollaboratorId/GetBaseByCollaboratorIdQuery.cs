using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ImprovementPlans.GetByCollaboratorId;

public record GetBaseByCollaboratorIdQuery
(
    string CollaboratorName,

    int WithResponses, // 0 = Both, 1 = Yes, 2 = No

    int Page,
    int PageSize
)
:
IRequest<ErrorOr<ImprovementPlansByCollaboratorResponse>>;
