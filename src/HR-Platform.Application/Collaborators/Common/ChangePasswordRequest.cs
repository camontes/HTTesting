namespace HR_Platform.Application.Collaborators.Common;

public record ChangePasswordRequest(
    string BusinessEmail,
    string OldPassword,
    string NewPassword
);
