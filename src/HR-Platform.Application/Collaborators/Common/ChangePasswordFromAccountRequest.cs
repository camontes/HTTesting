namespace HR_Platform.Application.Collaborators.Common;

public record ChangePasswordFromAccountRequest(
    string OldPassword,
    string NewPassword
);
