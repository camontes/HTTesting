namespace HR_Platform.Application.Benefits.Common;
public record BenefitFileResponse
(
    Guid IdFile,
    string TimePosted,
    string TimePostedEnglish,
    string TimePostedTolTip,
    string TimePostedTolTipEnglish,
    string Name,
    string Description,
    bool IsVisible,
    bool IsPinned,
    string CreatedFormat,
    string CreatedFormatEnglish,
    string UpdatedFormat,
    string UpdatedFormatEnglish,
    string UpdatedFormatToltip,
    string FileName,
    string FileURL,
    string CreationDateFile,
    string CreationDateFileEnglish,
    string ImageName,
    string ImageURL,
    string FullNameTh,
    DateTime CreationDate,
    bool IsAddedButton,
    string ButtonName,
    bool IsSurveyInclude,
    bool IsAvailableForAll,
    bool IsAnotherContraint,
    string AnotherContraint,
    int MinimumMonthsConstraint
);

