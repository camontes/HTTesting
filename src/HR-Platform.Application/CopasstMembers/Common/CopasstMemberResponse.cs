namespace HR_Platform.Application.CopasstMembers.Common;
public record CopasstMemberResponse
(
    Guid Id,
    string Name,
    string Position,
    string Photo,
    string ShortName
);

