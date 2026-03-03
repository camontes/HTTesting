namespace HR_Platform.Application.DreamMapQuestions.Common;

public record DreamMapQuestionsResponse(
    Guid Id,
    string Question,
    DateTime CreationDate
);
