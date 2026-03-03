namespace HR_Platform.Application.Tags.Common;
public record TagWIthCollaboratorCountResponse
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
