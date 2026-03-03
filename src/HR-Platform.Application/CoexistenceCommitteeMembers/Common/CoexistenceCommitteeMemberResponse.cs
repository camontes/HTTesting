namespace HR_Platform.Application.CoexistenceCommitteeMembers.Common;
public record CoexistenceCommitteeMemberResponse
(
    Guid Id,
    string Name,
    string Position,
    string Photo,
    string ShortName
);

