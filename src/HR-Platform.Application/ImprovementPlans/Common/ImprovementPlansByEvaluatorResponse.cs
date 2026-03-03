public record ImprovementPlansByEvaluatorResponse
(
    Guid EvaluatorId,

    string EvaluatorName,
    string EvaluatorShortName,

    string EvaluatorPhoto,

    string EvaluatorDocumentType,
    string EvaluatorDocumentTypeEnglish,

    string EvaluatorDocument,

    string EvaluatorPosition,
    string EvaluatorPositionEnglish,

    string EvaluatorEntranceDate,
    string EvaluatorEntranceDateEnglish,

    List<ImprovementPlanByEvaluatorResponse> ImprovementPlans
);

public record ImprovementPlanByEvaluatorResponse
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

    string ReferenceNumber,

    string ImprovementPlanCreationDateFormat,
    string ImprovementPlanCreationDateFormatEnglish,
    string AddedTimeFormatToltip,

    Guid ImprovementPlanId,

    string ColorFace,
    string GeneralScoreResult,

    List<ImprovementPlanTaskByEvaluatorResponse> ImprovementPlanTasks
);

public record ImprovementPlanTaskByEvaluatorResponse
(
    Guid TaskId,

    string ImprovementPlanTaskDescription,

    ImprovementPlanTaskResponseByCollaboratorResponse? ImprovementPlanTaskResponse,

    List<ImprovementPlanFileByEvaluatorResponse> ImprovementPlanFiles
);

public record ImprovementPlanFileByEvaluatorResponse
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



