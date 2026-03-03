using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.ResendInvitation;

public record ResendInvitationCommand(string BusinessEmail, string PersonalEmail) : IRequest<ErrorOr<ResendInvitationResponse>>;