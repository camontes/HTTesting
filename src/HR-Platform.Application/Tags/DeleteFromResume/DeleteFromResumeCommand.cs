using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Tags.DeleteFromResume;

public record DeleteFromResumeCommand(Guid CollaboratorTagId) : IRequest<ErrorOr<bool>>;