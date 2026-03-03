namespace HR_Platform.Application.Inductions.Common;
public record InductionForCollaboratorResponse
(
    string InductionId,
    string Name,
    string Description,
    string UpdatedFormat,
    string UpdatedFormatEnglish,
    string UpdatedToltipDate,
    //string DeleteFormat,
    //string DeleteFormatEnglish,
    //string DeleteToltipDate,
    //bool HasDeleted,
    string FullNameTh,
    bool IsVisible,
    bool AllowForAllCollaborators,
    DateTime CreacionDate,
    string InductionAnswerDate,
    string InductionAnswerDateEnglish,
    string InductionAnswerDateToltip,
    string InductionSendDate,
    string InductionSendDateEnglish,
    string InductionSendDateToltip,
    string InductionDeleteDate,
    string InductionDeleteDateEnglish,
    string InductionDeleteDateToltip,
    string NameWhoDeletedByTh,
    bool IsInductionFinished,
    List<InductionFileResponse> InductionFilesList
);

