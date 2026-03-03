using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateSocialSecurity;
public record UpdateSocialSecurityCommand(
    string EmailChangeBy,
    Guid CollaboratorId,
    string CompanyId,
    string? PensionId,
    string? SeveranceBenefitId,
    string? HealthEntityId

) : IRequest<ErrorOr<UpdateSocialSecurityResponse>>;
