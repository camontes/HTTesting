namespace HR_Platform.Application.DocumentManagements.Common;
public record DocumentManagementFileResponse
(
    Guid IdFile,
    string FileName,
    string FileURL,
    int FileTypeId,
    string FileTypeName,
    string Other,
    string TimePosted,
    string TimePostedEnglish,
    DateTime CreationDate,
    string CreationDateTooltip,
    string UrlPhotoTH,
    string FullNameTh,
    string ShortNameTh
);

