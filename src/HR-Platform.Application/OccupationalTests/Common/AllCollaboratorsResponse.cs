namespace HR_Platform.Application.OccupationalTests.Common;

public record AllCollaboratorsResponse(List<DataCollaborator> DataCollaboratorsList, int CollaboratorsCount);

public record DataCollaborator
(
    Guid CollaboratorId,
    string Document,
    string DocumentType,
    string OtherDocumentType,
    string Name,
    string IntranceDate,
    string Assignation,
    DateTime EntranceDate
);
