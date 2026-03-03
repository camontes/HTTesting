namespace HR_Platform.Application.Collaborators.Common;

public record RecoveryCodeResponse(
    string RecoveryCode,
    bool IsSuspended
);
