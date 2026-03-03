using HR_Platform.Application.DocumentManagements.Common;

namespace HR_Platform.Application.ContractTypes.Common;

public record DocumentManagementsResponse(
    Guid CollaboratorId,
    string Document,
    string DocumentType,
    string Name,
    string IntranceDate,
    string IntranceDateEnglish,
    List<DocumentManagementFileResponse> DocumentManagementFile
);
