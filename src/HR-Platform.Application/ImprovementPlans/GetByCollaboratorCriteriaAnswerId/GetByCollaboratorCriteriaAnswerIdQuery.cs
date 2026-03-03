using ErrorOr;
using HR_Platform.Application.ImprovementPlans.Common;
using MediatR;

namespace HR_Platform.Application.ImprovementPlans.GetByCollaboratorCriteriaAnswerId;

public record GetByCollaboratorCriteriaAnswerIdQuery(Guid CollaboratorCriteriaAnswerId) : IRequest<ErrorOr<List<ImprovementPlanResponse>>>;