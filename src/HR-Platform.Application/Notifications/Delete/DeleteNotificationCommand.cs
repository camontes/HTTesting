using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Notifications.Delete;

public record DeleteNotificationCommand
(
    Guid Id
)
:
IRequest<ErrorOr<bool>>;
