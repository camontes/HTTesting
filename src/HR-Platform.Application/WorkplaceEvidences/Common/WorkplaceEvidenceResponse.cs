using HR_Platform.Application.WorkplaceEvidences.Common;

namespace HR_Platform.Application.ContractTypes.Common;

public record WorkplaceEvidencesResponse(
    Guid CollaboratorId,
    List<WorkplaceEvidenceFileResponse> WorkplaceEvidenceFile,
    List<string> FileYears
);
