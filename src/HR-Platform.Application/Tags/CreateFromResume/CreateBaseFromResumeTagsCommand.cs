using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Tags.CreateFromResume;

public record CreateBaseFromResumeTagsCommand(string Name, Guid CollaboratorId) : IRequest<ErrorOr<bool>>;

