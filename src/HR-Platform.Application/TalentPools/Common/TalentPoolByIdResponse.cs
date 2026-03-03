using HR_Platform.Domain.Assignations;

namespace HR_Platform.Application.TalentPools.Common;

public record TalentPoolByIdResponse(
    string Id,
    string Tittle,
    string Description,
    string PostedTime,
    string PostedTimeEnglish,
    List<CollaboratorInfo> CollaboratorList
);


public record CollaboratorInfo
(
    string CollaboratorId,
    string Document,
    string DocumentType,
    string OtherDocumentType,
    string Name,
    string EntranceDate,
    string EntranceDateEnglish,
    string Assignation
);