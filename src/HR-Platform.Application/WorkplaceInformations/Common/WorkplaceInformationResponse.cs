using HR_Platform.Application.WorkplaceInformations.Common;

namespace HR_Platform.Application.ContractTypes.Common;

public record WorkplaceInformationsResponse(
    Guid CollaboratorId,
    string Document,
    string DocumentType,
    string Name,
    string IntranceDate,
    List<WorkplaceInformationFileResponse> WorkplaceInformationFile,
    List<string> FileYears
);
