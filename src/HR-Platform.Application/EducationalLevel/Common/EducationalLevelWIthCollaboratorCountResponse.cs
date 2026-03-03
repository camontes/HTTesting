namespace HR_Platform.Application.EducationalLevels.Common;
public record EducationalLevelWIthCollaboratorCountResponse
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
