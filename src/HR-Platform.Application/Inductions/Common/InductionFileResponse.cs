namespace HR_Platform.Application.Inductions.Common;
public record InductionFileResponse
(
    Guid IdFile,
    string FileName,
    string FileURL,
    string TimePosted,
    string TimePostedEnglish,
    string CreationDate,
    string CreationDateEnglish,
    string CreationDateToltip
);

