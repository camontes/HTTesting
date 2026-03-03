namespace HR_Platform.Application.BrigadeMembers.Common;

public record BrigadeMemberListResponse(
    Guid Id,
    string Name,
    string Photo,
    string Position,
    bool IsMainLeader,
    bool IsBrigadeLeader,
    int IconId
    
);
