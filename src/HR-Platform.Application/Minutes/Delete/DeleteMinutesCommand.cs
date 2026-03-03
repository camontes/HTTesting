using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Minutes.Delete;

public record DeleteMinutesCommand
(
    Guid MinuteId
) : IRequest<ErrorOr<bool>>;

