using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Notifications.MarkAllAsReadByCollaborator;

internal sealed class MarkAllAsReadByCollaboratorCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    INotificationRepository notificationRepository,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<MarkAllAsReadByCollaboratorCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly INotificationRepository _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(MarkAllAsReadByCollaboratorCommand command, CancellationToken cancellationToken)
    {
        DateTime colombianHour = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = colombianHour.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _collaboratorRepository.GetByEmailAsync(command.Email) is not Collaborator collaborator)
            return Error.NotFound("Collaborator.NotFound", "The Collaborator with the provide Id was not found.");

        if (await _notificationRepository.GetByCollaboratorIdAsync(collaborator.Id) is not List<Notification> oldNotifications)
            return Error.NotFound("Notification.NotFound", "The Notification with the provide Id was not found.");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Notification.EditionDate", "EditionDate is not valid");

        try
        {
            if(oldNotifications is not null && oldNotifications.Count > 0)
            {
                foreach (Notification oldNotification in oldNotifications)
                {
                    oldNotification.IsRead = true;

                    _notificationRepository.Update(oldNotification);

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
