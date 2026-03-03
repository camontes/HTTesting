namespace HR_Platform.Application.ImprovementPlans.Common;
public record ImprovementPlanFileResponse
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

