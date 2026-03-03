namespace HR_Platform.Application.QuestionTypes.Common;

public record QuestionTypesResponse(
    Guid Id,

    string Name,
    string NameEnglish,

    DateTime CreationDate
);
