using ErrorOr;
using MediatR;

namespace HR_Platform.Application.CopasstMembers.Create;

public record CreateCopasstMembersCommand(List<Guid> CollaboratorIds) : IRequest<ErrorOr<bool>>;



