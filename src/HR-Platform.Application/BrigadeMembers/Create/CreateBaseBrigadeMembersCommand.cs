using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.Create;

public record CreateBaseBrigadeMembersCommand(List<BrigadeMemberData> BrigadeMembersDataList) : IRequest<ErrorOr<bool>>;


