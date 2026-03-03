namespace HR_Platform.Application.CopasstMembers.Common;

public record CollaboratorCopasstListResponse(
    Guid Id,
    string Name,
    string Email,
    string BusinessEmail,
    string ShortName,
    string Photo
);
