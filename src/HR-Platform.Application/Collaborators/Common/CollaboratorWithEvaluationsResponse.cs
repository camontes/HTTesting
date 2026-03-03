namespace HR_Platform.Application.Collaborators.Common;

public record CollaboratorWithEvaluationsResponse(
    Guid Id,

    Guid CompanyId,

    Guid PositionId,
    
    string Document,

    string DocumentTypeName,
    string DocumentTypeNameEnglish,

    string Name,

    string BusinessEmail,
    string PersonalEmail,

    string PositionName,
    string PositionNameEnglish,

    string PhotoURL,
    string PhotoName,

    List<CriteriaResultByCollaboratorResponse> Evaluations,

    DateTime EntranceDate,

    string EntranceDateFormatMonthShort,
    string EntranceDateFormatMonthShortEnglish,

    string EntranceDateFormatSlash,
    string EntranceDateFormatSlashEnglish,

    DateTime EditionDate,

    string EditionDateFormatMonthLarge,
    string EditionDateFormatMonthLargeEnglish,

    string EditionDateTimeFormatMonthToltip,
    string EditionDateTimeFormatMonthToltipEnglish
);

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

