namespace HR_Platform.Application.BrigadeMembers.Common;

public record BrigadeMembersResponse(
    Guid Id,
    Guid CollaboratorId,
    string Name,
    string Email,
    string Position,
    string BrigadeName,
    string BrigadeNameEnglish,
    int BrigadeIconId,
    string PhotoUrl,
    string ShortName,
    bool IsMainLeader,
    bool IsBrigadeLeader
);
