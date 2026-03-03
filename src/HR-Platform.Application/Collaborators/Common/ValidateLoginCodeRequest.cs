namespace HR_Platform.Application.Collaborators.Common;

public record ValidateLoginCodeRequest(
    string LoginCode,
    string Email
);
