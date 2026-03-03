namespace HR_Platform.Application.DocumentManagements.Common;

public record AllCollaboratorsDocumentManagementResponse(List<DataCollaboratorDocumentManagement> DataCollaboratorsList, int CollaboratorsCount);

public record DataCollaboratorDocumentManagement
(
    Guid CollaboratorId,
    string Document,
    string DocumentType,
    string Name,
    string IntranceDate,
    string Assignation
);
