namespace HR_Platform.Application.OccupationalTests.Common;
public record OccupationalTestFileResponse
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

