namespace HR_Platform.Application.Roles.Common;

public record RolesResponse(
    Guid Id,

    Guid CompanyId,

    string Name,
    string NameEnglish,

    bool IsEditable,
    bool IsDeleteable,

    DateTime CreationDate,
    DateTime EditionDate
);
