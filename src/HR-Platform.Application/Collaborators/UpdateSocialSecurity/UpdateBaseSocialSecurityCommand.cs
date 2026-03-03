using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateSocialSecurity;
public record UpdateBaseSocialSecurityCommand(
    Guid CollaboratorId,
    string? PensionId,
    string? SeveranceBenefitId,
    string? HealthEntityId 

) : IRequest<ErrorOr<UpdateCollaboratorResponse>>;
