namespace HR_Platform.Application.EvidenceCoexistenceCommitteeVotes.Common;
public record EvidenceCoexistenceCommitteeVoteFileResponse
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

