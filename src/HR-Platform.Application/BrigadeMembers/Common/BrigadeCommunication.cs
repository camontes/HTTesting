namespace HR_Platform.Application.BrigadeMembers.Common;

public record BrigadeCommunication(
    bool IsVisible,
    List<BrigadeMembersResponse> MainLeaders,
    List<BrigadeMembersResponse> BrigadeLeaders,
    List<BrigadeMembersResponse> Members
);