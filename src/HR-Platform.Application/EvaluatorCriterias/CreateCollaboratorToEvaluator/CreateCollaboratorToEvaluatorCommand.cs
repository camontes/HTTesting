using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EvaluatorCriterias.CreateCollaboratorToEvaluator;

public record CreateCollaboratorToEvaluatorCommand
(
    Guid PositionId,
    Guid EvaluatorId, //CollaboratorId
    bool IsForAll,
    List<Guid>? CollaboratorIds
) : IRequest<ErrorOr<bool>>;


