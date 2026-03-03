namespace HR_Platform.Application.WorkplaceRecommendations.Common;
public record WorkplaceRecommendationFileResponse
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

