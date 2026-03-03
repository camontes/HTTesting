
using ErrorOr;
using HR_Platform.Application.EvaluatorCriterias.Common;
using MediatR;

namespace HR_Platform.Application.EvaluatorCriterias.GetCollaboratorByEvaluator;

public record GetBaseCriteriaResultByCollaboratorQuery(Guid CollaboratorId, bool IsInHistory) : IRequest<ErrorOr<List<CriteriaResultByCollaboratorResponse>>>;