namespace HR_Platform.Application.EvaluatorCriterias.Common;

public record CriteriaResultByCollaboratorResponse
(
    string CollaboratorCriteriaAnswerId,
    Guid CollaboratorCriteriaId,
    string ReferenceNumber,
    double GeneralScoreResult,
    string Colorface,
    string EvaluatorName,
    string EvaluatorShortName,
    string EvaluatorPhoto,
    string AddedTimeFormat,
    string AddedTimeFormatEnglish,
    string AddedTimeFormatToltip,
    string Position,
    string PositionEnglish,
    string Recommendation,
    CriteriaResult CriteriaObjetiveResult,
    CriteriaResult CriteriaSubjetiveResult,
    bool RequireImprovementPlan,
    DateTime CreaationDate
);

public record CriteriaResult
(
    int CriteriaPercentage,
    double CriteriaPercentageTotal,
    double ScoreTotal,
    List<CriteriaAnswer> CriteriaAnswerList
);

public record CriteriaAnswer
(
    string CriteriaDescriptionSection,
    string CriteriaDescriptionSectionEnglish,
    int CriteriaDescriptionSectionPercentage,
    int Value,
    string Colorface,
    int ColorfaceIndex,
    string DescriptionAnswer,
    string DescriptionAnswerEnglish
);