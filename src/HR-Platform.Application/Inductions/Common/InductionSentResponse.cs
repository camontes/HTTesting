namespace HR_Platform.Application.Inductions.Common;
public record InductionSentResponse
(
    string CollaboratorId,
    string DocumentType,
    string Document,
    string Name,
    DateTime EntranceDateTimeFormat,
    string EntranceDate,
    string EntranceDateEnglish,
    string Assignation,
    int CountInductionsSent,
    List<InductionSentDetailResponse> DetailInductionList
);

