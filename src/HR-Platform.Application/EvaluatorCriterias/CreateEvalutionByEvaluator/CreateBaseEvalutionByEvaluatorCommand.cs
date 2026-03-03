using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EvaluatorCriterias.CreateEvalutionByEvaluator;

public record CreateBaseEvalutionByEvaluatorCommand
(
    Guid CollaboratorCriteriaId,
    string PositionName,
    string PositionNameEnglish,
    int ObjectiveCriteriaValue,
    int SubjectiveCriteriaValue,
    List<CriteriaAnswer> CriteriaAnswerList,
    string Comments
) : IRequest<ErrorOr<bool>>;


public record CriteriaAnswer
(
    int EvaluationCriteriaTypeId,
    string CriteriaName,
    string CriteriaNameEnglish,
    int CriteriaPercentage,
    string CriteriaScoreDescription,
    string CriteriaScoreDescriptionEnglish,
    int CriteriaSingleScorePercentage,
    int CriteriaScoreIndexAnswer
);