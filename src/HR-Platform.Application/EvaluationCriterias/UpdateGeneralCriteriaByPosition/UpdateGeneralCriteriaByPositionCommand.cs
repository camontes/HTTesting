using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EvaluationCriterias.UpdateGeneralCriteriaByPosition;

public record UpdateGeneralCriteriaByPositionCommand(Guid PositionId, int SubjectiveCriteria, int ObjectiveCriteria) : IRequest<ErrorOr<bool>>;