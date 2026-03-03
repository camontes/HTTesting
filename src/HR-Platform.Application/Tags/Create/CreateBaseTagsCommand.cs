using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Tags.Create;

public record CreateBaseTagsCommand(List<BaseTagCommand> TagsList) : IRequest<ErrorOr<bool>>;

public record BaseTagCommand(
    string Name,
    string NameEnglish
);

