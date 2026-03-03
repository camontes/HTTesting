namespace HR_Platform.Application.EducationalLevels.Common;

public record EducationalLevelsResponse(
    Guid Id,

    Guid CompanyId,

    string Name,
    string NameEnglish,

    bool IsEditable,
    bool IsDeleteable,

    DateTime CreationDate,
    DateTime EditionDate
);
