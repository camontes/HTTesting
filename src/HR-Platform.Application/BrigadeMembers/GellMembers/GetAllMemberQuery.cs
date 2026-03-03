using ErrorOr;
using MediatR;
using HR_Platform.Application.BrigadeMembers.Common;

namespace HR_Platform.Application.BrigadeMembers.GetGellMember;

public record GetAllMemberQuery() : IRequest<ErrorOr<BrigadeCommunication>>;