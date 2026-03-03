using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Tags.Delete;

public record BaseDeleteTagsCommand(List<Guid> TagsList) : IRequest<ErrorOr<bool>>;

