using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Tags.CreateFromResume;

public record CreateFromResumeTagsCommand(string Name, Guid CollaboratorId, Guid CompanyId) : IRequest<ErrorOr<bool>>;

