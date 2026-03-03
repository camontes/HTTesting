using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.Delete;

public record DeleteBrigadeMembersCommand() : IRequest<ErrorOr<bool>>;



