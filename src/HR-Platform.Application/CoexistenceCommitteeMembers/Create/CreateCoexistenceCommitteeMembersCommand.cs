using ErrorOr;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMembers.Create;

public record CreateCoexistenceCommitteeMembersCommand(List<Guid> CollaboratorIds) : IRequest<ErrorOr<bool>>;



