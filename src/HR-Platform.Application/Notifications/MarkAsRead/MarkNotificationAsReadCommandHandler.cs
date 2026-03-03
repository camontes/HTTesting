using ErrorOr;
using HR_Platform.Application.BrigadeInventories.MarkAsDeleted;
using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.BrigadeInventoryFiles;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Platform.Application.Notifications.MarkAsRead;

internal sealed class MarkNotificationAsReadCommandHandler(
    INotificationRepository notificationRepository,

    IUnitOfWork unitOfWork
    ) : IRequestHandler<MarkNotificationAsReadCommand, ErrorOr<bool>>
{
    private readonly INotificationRepository _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
    
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(MarkNotificationAsReadCommand command, CancellationToken cancellationToken)
    {
        DateTime colombianHour = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string editionDateString = colombianHour.ToString("MM/dd/yyyy HH:mm:ss");

        if (await _notificationRepository.GetByIdAsync(new NotificationId(command.Id)) is not Notification oldNotification)
            return Error.NotFound("Notification.NotFound", "The Notification with the provide Id was not found.");

        if (TimeDate.Create(editionDateString) is not TimeDate editionDate)
            return Error.Validation("Notification.EditionDate", "EditionDate is not valid");

        try
        {
            oldNotification.IsRead = true;

            _notificationRepository.Update(oldNotification);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
