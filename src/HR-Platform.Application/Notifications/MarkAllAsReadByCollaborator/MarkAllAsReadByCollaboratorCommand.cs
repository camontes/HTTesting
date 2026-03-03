using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Notifications.MarkAllAsReadByCollaborator;

public record MarkAllAsReadByCollaboratorCommand
(
    string Email
)
:
IRequest<ErrorOr<bool>>;
