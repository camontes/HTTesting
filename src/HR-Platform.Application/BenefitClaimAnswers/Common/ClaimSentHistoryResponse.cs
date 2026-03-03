namespace HR_Platform.Application.BenefitClaimAnswers.Common;
public record ClaimSentHistoryResponse
(
    string BenefitClaimAnswerId,
    string ReferenceNumber,
    string BenefitName,
    bool IsBenefitAccepeted,
    string ClaimDate,
    string ClaimDateEnglish,
    string ClaimDateToltip,
    bool IsAnotherContraint,
    string AnotherContraint,
    bool IsAvailableForAll,
    string MinimumMonthsConstraint,
    string CloseDate,
    string CloseDateEnglish,
    string CloseDateToltip,
    bool HasDeleted,
    string DeletedDate,
    string DeletedDateEnglish,
    string DeletedDateToltip,
    string NameWhoDeletedClaim,
    string NameWhoManagedClaim,
    string NameShortWhoManagedClaim,
    string PhotoWhoManagedClaim,
    string Details,
    DateTime CreacionDateFormat
);

