namespace HR_Platform.Application.Inductions.Common;
public record InductionResponse
(
    string InductionId,
    string Name,
    string Description,
    string UpdatedFormat,
    string UpdatedFormatEnglish,
    string UpdatedToltipDate,
    string FullNameTh,
    bool IsVisible,
    bool AllowForAllCollaborators,
    DateTime CreacionDate,
    List<InductionFileResponse> InductionFilesList
);

