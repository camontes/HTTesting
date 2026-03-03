public record ImprovementPlansByCollaboratorResponse
(
    Guid CollaboratorId,

    string CollaboratorName,
    string CollaboratorShortName,

    string CollaboratorPhoto,

    string CollaboratorDocumentType,
    string CollaboratorDocumentTypeEnglish,
    string CollaboratorDocument,

    string CollaboratorPosition,
    string CollaboratorPositionEnglish,

    string CollaboratorEntranceDate,
    string CollaboratorEntranceDateEnglish,

    List<ImprovementPlanByCollaboratorResponse> ImprovementPlans
);

public record ImprovementPlanByCollaboratorResponse
(
    Guid ImprovementPlanId,

    string ReferenceNumber,

    string ImprovementPlanCreationDateFormat,
    string ImprovementPlanCreationDateFormatEnglish,
    string AddedTimeFormatToltip,

    string ColorFace,
    string GeneralScoreResult,

    List<ImprovementPlanTaskByCollaboratorResponse> ImprovementPlanTasks
);

public record ImprovementPlanTaskByCollaboratorResponse
(
    Guid TaskId,

    string ImprovementPlanTaskDescription,

    ImprovementPlanTaskResponseByCollaboratorResponse? ImprovementPlanTaskResponse,

    List<ImprovementPlanFileByCollaboratorResponse> ImprovementPlanFiles
);

public record ImprovementPlanTaskResponseByCollaboratorResponse
(
    Guid TaskId,

    string ImprovementPlanTaskResponseDescription,

    List<ImprovementPlanTaskResponseFileByCollboratorResponse> ImprovementPlanTaskResponseFiles
);

public record ImprovementPlanFileByCollaboratorResponse
(
    Guid IdFile,

    string FileName,
    string FileURL,

    string FullNameTH,
    string ShortNameTH,

    string EmailTH,

    string URLPhotoTH,

    string TimePosted,
    string TimePostedEnglish,

    string TimePostedTolTip,
    string TimePostedTolTipEnglish
);

public record ImprovementPlanTaskResponseFileByCollboratorResponse
(
    Guid IdFile,

    string FileName,
    string FileURL,

    string FullNameTH,
    string ShortNameTH,

    string EmailTH,

    string URLPhotoTH,

    string TimePosted,
    string TimePostedEnglish,

    string TimePostedTolTip,
    string TimePostedTolTipEnglish
);

