using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Notifications.MarkAsRead;

public record MarkNotificationAsReadCommand
(
    Guid Id
)
:
IRequest<ErrorOr<bool>>;
