using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EvaluationCriteriaScores.UpdateByEvaluationCriteria;

public record UpdateCriteriaScoresByCriteriaCommand(
    Guid EvaluationCriteriaId,
    Guid PositionId,
    List<EvaluationCriteriasScoreCommand> EvaluationCriteriaScores
) : IRequest<ErrorOr<bool>>;

public record EvaluationCriteriasScoreCommand(
    Guid EvaluationCriteriaScoreId,
    string Description,
    string DescriptionEnglish,
    int LowerScore,
    int UpperScore
);