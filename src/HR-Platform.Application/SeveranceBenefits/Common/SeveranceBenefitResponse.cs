namespace HR_Platform.Application.SeveranceBenefits.Common;

public record SeveranceBenefitsResponse(
    Guid Id,

    Guid CompanyId,

    string Name,
    string NameEnglish,

    bool IsEditable,
    bool IsDeleteable,

    DateTime CreationDate,
    DateTime EditionDate
);
