namespace HR_Platform.Application.Forms.Common;

public record NoveltyByCollaboratorResponse
(
    Guid FormAnswerId,

    Guid CollaboratorId,

    string CollaboratorName,
    
    string Document,

    string DocumentTypeName,
    string DocumentTypeNameEnglish,

    string AssingnationName,
    string AssingnationNameEnglish,

    string ReferenceNumber,

    Guid FormAnswerGroupId,
    int FormAnswerStateGroupId,

    string NoveltyType,
    string NoveltyTypeEnglish,

    string ApplicationType,

    string ApplicationDate,
    string ApplicationDateEnglish,

    string ApplicationDateToltip,
    string ApplicationDateToltipEnglish,

    DateTime CreationTime,

    string EntranceDate,
    string EntranceDateEnglish,

    string EntranceDateToltip,
    string EntranceDateToltipEnglish,

    List<AnswerFormObject> Answers
);

public record AnswerFormObject
(
    string Question,
    string Answer,

    bool IsRequired
);

