using ErrorOr;
using HR_Platform.Application.BrigadeMembers.Create;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.Update;

public record UpdateBrigadeMembersCommand(List<BrigadeMemberData> BrigadeMembersDataList, Guid CompanyId) : IRequest<ErrorOr<bool>>;



