using ErrorOr;
using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.Notifications.Delete;

internal sealed class DeleteNotificationCommandHandler
(
    INotificationRepository notificationRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<DeleteNotificationCommand, ErrorOr<bool>>
{
    private readonly INotificationRepository _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(DeleteNotificationCommand command, CancellationToken cancellationToken)
    {
        if (await _notificationRepository.GetByIdAsync(new NotificationId(command.Id)) is not Notification oldNotification)
            return Error.NotFound("Notification.NotFound", "The Notification with the provide Id was not found.");

        try
        {
            _notificationRepository.Delete(oldNotification);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}