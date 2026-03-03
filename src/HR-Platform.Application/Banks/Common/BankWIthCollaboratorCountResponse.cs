namespace HR_Platform.Application.Banks.Common;
public record BankWIthCollaboratorCountResponse
(
    Guid Id,

    Guid CompanyId,

    string Name,
    string NameEnglish,

    int NumberOfCollaborators,

    bool IsEditable,
    bool IsDeleteable,

    DateTime CreationDate,
    DateTime EditionDate
);
