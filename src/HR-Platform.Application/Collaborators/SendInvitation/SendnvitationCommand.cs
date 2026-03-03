using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.SendInvitation;

public record SendInvitationCommand(string BusinessEmail, string PersonalEmail) : IRequest<ErrorOr<SendInvitationResponse>>;