namespace HR_Platform.Application.Pensions.Common;
public record PensionWIthCollaboratorCountResponse
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
