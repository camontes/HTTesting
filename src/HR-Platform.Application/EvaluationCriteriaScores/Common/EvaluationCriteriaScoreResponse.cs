namespace HR_Platform.Application.EvaluationCriteriaScores.Common;

public record EvaluationCriteriaScoreResponse
(
    Guid PositionId,

    string PositionName,
    string PositionNameEnglish,

    int SubjectiveCriteriaValue,
    int ObjectiveCriteriaValue,

    List<CriteriasResponse> ObjectiveCriterias,
    List<CriteriasResponse> SubjectiveCriterias,

    string EditionTimeFormat,
    string EditionTimeFormatEnglish,
    string EditionTimeFormatTooltip
);

public record CriteriasResponse
(
    Guid EvaluationCriteriaId,

    int EvaluationCriteriaTypeId,

    string CriteriaName,
    string CriteriaNameEnglish,

    string CriteriaDescription,
    string CriteriaDescriptionEnglish,

    int CriteriaPercentage,

    List<CriteriasScoreResponse> CriteriasScore
);

public record CriteriasScoreResponse
(
    Guid EvaluationCriteriaScoreId,

    Guid EvaluationCriteriaId,

    string CriteriaDescription,
    string CriteriaDescriptionEnglish,

    int LowerScore,
    int UpperScore
);
