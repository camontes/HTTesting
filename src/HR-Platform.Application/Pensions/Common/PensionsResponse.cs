namespace HR_Platform.Application.Pensions.Common;

public record PensionsResponse(
    Guid Id,

    Guid CompanyId,

    string Name,
    string NameEnglish,

    bool IsEditable,
    bool IsDeleteable,

    DateTime CreationDate,
    DateTime EditionDate
);
