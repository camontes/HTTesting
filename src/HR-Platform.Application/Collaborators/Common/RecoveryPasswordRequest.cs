namespace HR_Platform.Application.Collaborators.Common;

public record RecoveryPasswordRequest(
    string BusinessEmail,
    string RecoveryCode,
    string OldPassword,
    string NewPassword
);
