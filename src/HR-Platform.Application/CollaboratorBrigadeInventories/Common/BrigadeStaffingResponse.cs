namespace HR_Platform.Application.CollaboratorBrigadeInventories.Common;

public record BrigadeStaffingResponse
(
    Guid CollaboratorId,
    string FullNameTh,
    string TimeUpdate,
    string TimeUpdatedEnglish,
    string TimeUpdatedTolTip,
    string Name,
    string BrigadeName,
    string Assignation,
    string Document,
    string DocumentType,
    string OtherDocumentType,
    string EntranceDate,
    string EntranceDateEnglish,
    List<CollaboratorBrigadeInventoryResponse> DetailBrigades
);

