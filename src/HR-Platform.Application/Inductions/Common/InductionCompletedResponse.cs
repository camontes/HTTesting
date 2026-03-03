namespace HR_Platform.Application.Inductions.Common;
public record InductionCompletedResponse
(
    string CollaboratorId,
    string DocumentType,
    string Document,
    string Name,
    DateTime EntranceDateTimeFormat,
    string EntranceDate,
    string EntranceDateEnglish,
    string Assignation,
    int CountInductionsCompleted,
    List<InductionCompletedHistoryResponse> DetailInductionList
);

