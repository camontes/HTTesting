namespace HR_Platform.Application.Collaborators.Common;

public record LoginCodeResponse(
    string LoginCode,
    bool AlreadyLogin,
    bool IsSuspended
);
