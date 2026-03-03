namespace HR_Platform.Application.ProfessionalAdvices.Common;
public record ProfessionalAdviceWIthCollaboratorCountResponse
(
    Guid Id,

    Guid CompanyId,

    string Name,
    string NameEnglish,

    string NameAcronyms,
    string NameAcronymsEnglish,

    int NumberOfCollaborators,

    bool IsEditable,
    bool IsDeleteable,

    DateTime CreationDate,
    DateTime EditionDate
);
