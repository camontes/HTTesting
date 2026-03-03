namespace HR_Platform.Application.NewCommunications.Common;
public record NewCommunicationFileResponse
(
    Guid IdFile,
    string TimePosted,
    string TimePostedEnglish,
    string TimePostedTolTip,
    string TimePostedTolTipEnglish,
    string Name,
    string Description,
    bool IsVisible,
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
    bool IsSurveyInclude
);

