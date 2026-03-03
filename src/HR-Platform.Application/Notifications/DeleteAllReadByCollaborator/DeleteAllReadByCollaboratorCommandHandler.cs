using ErrorOr;
using HR_Platform.Application.Notifications.MarkAllAsReadByCollaborator;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Notifications.DeleteAllReadByCollaborator;

internal sealed class DeleteAllReadByCollaboratorCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    INotificationRepository notificationRepository,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteAllReadByCollaboratorCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly INotificationRepository _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteAllReadByCollaboratorCommand command, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(command.Email) is not Collaborator collaborator)
            return Error.NotFound("Collaborator.NotFound", "The Collaborator with the provide Id was not found.");

        if (await _notificationRepository.GetAllReadByCollaboratorIdAsync(collaborator.Id) is not List<Notification> oldNotifications)
            return Error.NotFound("Notification.NotFound", "The Notification with the provide Id was not found.");

        try
        {
            if (oldNotifications is not null && oldNotifications.Count > 0)
            {
                foreach (Notification oldNotification in oldNotifications)
                {
                    oldNotification.IsRead = true;

                    _notificationRepository.Delete(oldNotification);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}