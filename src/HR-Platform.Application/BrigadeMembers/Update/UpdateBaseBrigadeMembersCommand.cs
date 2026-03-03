using ErrorOr;
using HR_Platform.Application.BrigadeMembers.Create;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.Update;

public record UpdateBaseBrigadeMembersCommand(List<BrigadeMemberData> BrigadeMembersDataList) : IRequest<ErrorOr<bool>>;


