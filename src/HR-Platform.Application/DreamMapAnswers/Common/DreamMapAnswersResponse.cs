namespace HR_Platform.Application.DreamMapAnswers.Common;

public record DreamMapAnswersResponse(
    string DreamMapId,
    List<DreamMapAnswersAllResponse> AllQuestionsAndAnswers,
    int TemplateIndicator,
    string CollaboratorName,
    string DateCompletionForm,
    string DateCompletionFormEnglish,
    bool SaveCurrent
);


public record DreamMapAnswersAllResponse(
    Guid Id,
    string CollaboratorId,
    string Question,
    string Answer
);