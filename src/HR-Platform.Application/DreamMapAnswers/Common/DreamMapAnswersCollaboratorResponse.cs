namespace HR_Platform.Application.DreamMapAnswers.Common;

public record DreamMapAnswersCollaboratorResponse(
   string Document,
   string DocumentType,
   string OtherDocumentType,
   string Name,
   string EntranceDate,
   string EntranceDateEnglish,
   string Assignation,
   string AssignationEnglish,
   Guid CollaboratorId,
   DateTime EntranceDateTime
);
