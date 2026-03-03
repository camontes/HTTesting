namespace HR_Platform.Application.Inductions.Common;
public record InductionCompletedHistoryResponse
(
    string InductionId,
    string Name,
    string Description,
    string FinishFormat,
    string FinishFormatEnglish,
    string FinishToltipDate,
    string UpdatedFormat,
    string UpdatedFormatEnglish,
    string UpdatedToltipDate,
    string DeletedFormat,
    string DeletedFormatEnglish,
    string DeletedToltipDate,
    bool HasDeleted,
    string NameWhoDeletedByTh,
    string FullNameTh,
    DateTime CreacionDate,
    List<InductionFileResponse> InductionFilesList
);

