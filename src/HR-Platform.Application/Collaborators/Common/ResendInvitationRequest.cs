namespace HR_Platform.Application.Collaborators.Common;

public record ResendInvitationRequest(
    string BusinessEmail,
    string PersonalEmail
);
