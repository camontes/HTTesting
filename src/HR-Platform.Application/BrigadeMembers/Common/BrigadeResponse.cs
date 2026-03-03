namespace HR_Platform.Application.BrigadeMembers.Common;

public record BrigadeResponse(
    List<BrigadeMembersResponse> MainLeaders,
    List<BrigadeMembersResponse> BrigadeLeaders,
    List<BrigadeMembersResponse> Members,
    bool IsVisible
);
