namespace HR_Platform.Application.WorkplaceEvidences.Common;
public record WorkplaceEvidenceFileResponse
(
    Guid IdFile,
    string FileName,
    string FileURL,
    string TimePosted,
    string TimePostedEnglish,
    DateTime CreationDate,
    string CreationDateTooltip,
    string UrlPhotoTH,
    string FullNameTh,
    string ShortNameTh
);

