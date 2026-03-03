using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.DeleteEducationFile;

public record DeleteEducationFileCommand(Guid CollaboratorId, Guid CollaboratorEducationId) : IRequest<ErrorOr<bool>>;



