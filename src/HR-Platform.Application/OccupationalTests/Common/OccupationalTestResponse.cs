using HR_Platform.Application.OccupationalTests.Common;

namespace HR_Platform.Application.ContractTypes.Common;

public record OccupationalTestsResponse(
    Guid CollaboratorId,
    string Document,
    string DocumentType,
    string OtherDocumentType,
    string Name,
    string IntranceDate,
    List<OccupationalTestFileResponse> OccupationalTestFile,
    List<string> FileYears
);
