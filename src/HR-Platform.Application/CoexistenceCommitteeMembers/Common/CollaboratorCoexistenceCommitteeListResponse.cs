namespace HR_Platform.Application.CoexistenceCommitteeMembers.Common;

public record CollaboratorCoexistenceCommitteeListResponse(
    Guid Id,
    string Name,
    string Email,
    string BusinessEmail,
    string ShortName,
    string Photo
);
