namespace HR_Platform.Application.Forms.Common;
public record FormQuestionsResponse
(
    string FormName,
    string FormDescription,
    string NoveltyType,
    string NoveltyTypeEnglish,
    List<QuestionsInForms> Questions
);

public record QuestionsInForms
(
    Guid QuestionId,
    string Question,
    bool IsRequired,
    string QuestionType
);

