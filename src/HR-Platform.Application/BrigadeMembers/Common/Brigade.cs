namespace HR_Platform.Application.BrigadeMembers.Common;

public record Brigade(
    BrigadeMembersResponse? Leader,
    List<BrigadeMembersResponse>? Members
);