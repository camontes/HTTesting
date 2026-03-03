using ErrorOr;
using HR_Platform.Application.EvaluationCriterias.Common;
using MediatR;

namespace HR_Platform.Application.EvaluationCriterias.GetByPosition;

public record GetEvaluationCriteriaByPositionQuery(Guid PositionId) : IRequest<ErrorOr<EvaluationCriteriaResponse>>;