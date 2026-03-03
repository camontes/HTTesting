namespace HR_Platform.Application.BenefitClaims.Common;

public record BenefitClaimsResponse(
    Guid BenefitClaimId,
    Guid CollaboratorId,
    string DocumentType,
    string OtherDocumentType,
    string Document,
    string Name,
    string Assignation,
    string EntranceDate,
    string EntranceDateEnglish,
    string ClaimDate,
    string ClaimDateEnglish,
    string ClaimDateEnglishToltip,
    string BenefitName,
    DateTime EditionDate
);
