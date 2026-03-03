using ErrorOr;
using MediatR;

namespace HR_Platform.Application.CopasstMembers.Create;

public record RemoveMemberCommand(Guid CollaboratorId) : IRequest<ErrorOr<bool>>;



