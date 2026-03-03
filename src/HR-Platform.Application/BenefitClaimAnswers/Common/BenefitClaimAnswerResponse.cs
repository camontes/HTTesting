namespace HR_Platform.Application.BenefitClaimAnswers.Common;

public record BenefitClaimsAnswerResponse
(
    Guid BenefitClaimAnswerId,

    Guid CompanyId,

    Guid CollaboratorId,

    string BenefitName,
    string Details,
    string ReferenceNumber,

    bool IsBenefitAccepeted,

    bool IsEditable,
    bool IsDeleteable,


    DateTime CreationDate,
    DateTime EditionDate,
    DateTime ManagementDate
);
