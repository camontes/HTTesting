namespace HR_Platform.Application.OccupationalRecommendations.Common;
public record OccupationalRecommendationFileResponse
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

