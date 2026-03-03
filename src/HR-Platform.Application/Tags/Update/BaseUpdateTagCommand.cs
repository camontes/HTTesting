using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Tags.Update;

public record BaseUpdateTagCommand(
     string Id,

    string Name,
    string NameEnglish
) : IRequest<ErrorOr<bool>>;

