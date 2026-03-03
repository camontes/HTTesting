namespace HR_Platform.Application.ActiveBreaks.Common;

public record ActiveBreakResponse
(
    Guid Id,

    string Name,
    string Description,

    bool IsVisible,
    bool IsPinned,

    bool IsEditable,
    bool IsDeleteable,

    string EmailWhoChangedByHR,
    string NameWhoChangedByHR,

    DateTime CreationDate,

    string CreatedFormat,
    string CreatedFormatEnglish,

    string CreatedTolTip,
    string CreatedToltipEnglish,

    DateTime EditionDate,

    string EditedFormat,
    string EditedFormatEnglish,

    string EditedTolTip,
    string EditedPostedEnglish,

    string? FileName,
    string? FileURL,

    DateTime? CreationDateFile,

    string CreatedFileFormat,
    string CreatedFileFormatEnglish,

    string CreatedFileTolTip,
    string CreatedFileToltipEnglish,

    string? ImageName,
    string? ImageURL,

    DateTime? CreationDateImage,

    string CreatedImageFormat,
    string CreatedImageFormatEnglish,

    string CreatedImageTolTip,
    string CreatedImageToltipEnglish
);

