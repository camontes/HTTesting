namespace HR_Platform.Application.BrigadeMembers.Common;

public record CollaboratorListResponse(
    Guid Id,
    string Name,
    string Email,
    string BusinessEmail,
    string ShortName,
    string Photo
);
