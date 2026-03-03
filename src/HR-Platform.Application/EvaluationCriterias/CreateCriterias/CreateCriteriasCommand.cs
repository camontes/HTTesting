using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EvaluationCriterias.CreateCriterias;

public record CreateCriteriasCommand(
    Guid PositionId,

    List<BaseEvaluationCriterias> CriteriasList
    ) : IRequest<ErrorOr<bool>>;

public record BaseEvaluationCriterias(
    string Id,

    int EvaluationCriteriaTypeId,

    string Name,
    string Description,

    int Percentage
);