namespace HR_Platform.Application.SeveranceBenefits.Common;

public record SeveranceBenefitWithCollaboratorCountResponse
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
