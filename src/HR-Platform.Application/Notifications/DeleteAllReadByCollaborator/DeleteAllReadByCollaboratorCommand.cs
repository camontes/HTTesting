using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Notifications.DeleteAllReadByCollaborator;

public record DeleteAllReadByCollaboratorCommand
(
    string Email
)
:
IRequest<ErrorOr<bool>>;

