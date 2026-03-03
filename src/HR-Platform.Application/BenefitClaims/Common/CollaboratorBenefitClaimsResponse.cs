namespace HR_Platform.Application.BenefitClaims.Common;

public record CollaboratorBenefitClaimsResponse(
    string CollaboratorId,
    string DocumentType,
    string OtherDocumentType,
    string Document,
    string Name,
    string EntranceDate,
    string EntranceDateEnglish,
    string ReferenceNumber,
    string UpdatedBenefitDate,
    string UpdatedBenefitDateEnglish,
    string UpdatedBenefitDateToltip,
    string BenefitName,
    string ClaimDate,
    string ClaimDateEnglish,
    string ClaimDateToltip,
    bool IsAvailableForAll,
    string MinimumMonthsConstraintBenefit,
    string AnotherContraintBenefit
);
