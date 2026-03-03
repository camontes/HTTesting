namespace HR_Platform.Application.ProfessionalAdvices.Common;

public record ProfessionalAdvicesResponse(
    Guid Id,

    Guid CompanyId,

    string Name,
    string NameEnglish,

    string NameAcronyms,
    string NameAcronymsEnglish,

    bool IsEditable,
    bool IsDeleteable,

    DateTime CreationDate,
    DateTime EditionDate
);
