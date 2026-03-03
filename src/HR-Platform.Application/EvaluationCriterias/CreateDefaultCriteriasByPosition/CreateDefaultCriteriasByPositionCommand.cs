using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EvaluationCriterias.CreateDefaultCriteriasByPosition;

public record CreateDefaultCriteriasByPositionCommand(Guid PositionId) : IRequest<ErrorOr<bool>>;