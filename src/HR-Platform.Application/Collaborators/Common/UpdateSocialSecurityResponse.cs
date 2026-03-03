using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Application.Collaborators.Common;
public record UpdateSocialSecurityResponse
(
    string? PensionId,
    string? SeveranceBenefitId,
    string? HealthEntityId,
    TimeDate? EditationDate
);
