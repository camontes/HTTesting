namespace HR_Platform.Application.EvaluatorCriterias.Common;

public record CollaboratorByEvalutorResponse
(
    Guid CollaboratorCriteriaId,
    Guid CollaboratorId,
    string DocumentType,
    string OtherDocumentType,
    string Document,
    string Name,
    string EntranceDate,
    string EntranceDateEnglish,
    Guid PositionId,
    string Position,
    string PositionEnglish
);
