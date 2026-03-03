using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Tags.AddCollaborator;

public record AddCollaboratorTagCommand(Guid TagId, Guid CollaboratorId) : IRequest<ErrorOr<bool>>;