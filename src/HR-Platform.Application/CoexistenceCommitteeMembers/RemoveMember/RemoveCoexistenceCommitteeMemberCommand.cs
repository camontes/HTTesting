using ErrorOr;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMembers.RemoveMember;

public record RemoveCoexistenceCommitteeMemberCommand(Guid CollaboratorId) : IRequest<ErrorOr<bool>>;



