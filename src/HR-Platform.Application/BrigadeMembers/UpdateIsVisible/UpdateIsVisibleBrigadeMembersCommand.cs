using ErrorOr;
using MediatR;

namespace HR_Platform.Application.BrigadeMembers.UpdateIsVisible;

public record UpdateIsVisibleBrigadeMembersCommand() : IRequest<ErrorOr<bool>>;