using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EvaluatorCriterias.CreateEvalutionByEvaluator;

public record CreateEvalutionByEvaluatorCommand(
    string EmailEvaluateBy,
    Guid CollaboratorCriteriaId,
    string PositionName,
    string PositionNameEnglish,
    int ObjectiveCriteriaValue,
    int SubjectiveCriteriaValue,
    List<CriteriaAnswer> CriteriaAnswerList,
    string Comments
) : IRequest<ErrorOr<bool>>;

