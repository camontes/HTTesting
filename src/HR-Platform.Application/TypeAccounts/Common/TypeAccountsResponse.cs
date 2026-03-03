namespace HR_Platform.Application.TypeAccounts.Common;

public record TypeAccountsResponse(
    Guid Id,

    Guid CompanyId,

    string Name,
    string NameEnglish,

    bool IsEditable,
    bool IsDeleteable,

    DateTime CreationDate,
    DateTime EditionDate
);
