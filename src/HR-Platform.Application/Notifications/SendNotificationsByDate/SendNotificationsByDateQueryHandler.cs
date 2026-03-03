using ErrorOr;
using HR_Platform.Application.Notifications.SendNotificationsByDate;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.NotificationTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Notifications.GetAllByCollaboratorId;


internal sealed class SendNotificationsByDateQueryHandler
(
    ICollaboratorRepository collaboratorRepository,
    INotificationRepository notificationRepository,
    INotificationTypeRepository notificationTypeRepository,

    IUnitOfWork unitOfWork
)
:
IRequestHandler<SendNotificationsByDateQuery, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly INotificationRepository _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
    private readonly INotificationTypeRepository _notificationTypeRepository = notificationTypeRepository ?? throw new ArgumentNullException(nameof(notificationTypeRepository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(SendNotificationsByDateQuery query, CancellationToken cancellationToken)
    {
        DateTime colombianTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = colombianTime.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("BenefitClaimAnswer.CreationDate", "CreationDate is not valid");

        if (TimeDate.Create(creationDateString) is not TimeDate editionDate)
            return Error.Validation("BenefitClaimAnswer.EditionDate", "EditionDate is not valid");

        List<Collaborator> collaborators = await _collaboratorRepository.GetAll();

        if (await _notificationTypeRepository.GetByIdAsync(new NotificationTypeId(2)) is not NotificationType notificationType)
            return Error.Validation("NotificationType.Id", "NotificationType with the provide Id was not found");

        try
        {
            if (collaborators is not null && collaborators.Count > 0)
            {
                foreach (Collaborator collaborator in collaborators)
                {
                    if(collaborator.Birthdate.Value.Day == query.Birthdate.Day && collaborator.Birthdate.Value.Month == query.Birthdate.Month)
                    {
                        Notification notification = new
                        (
                            new NotificationId(Guid.NewGuid()),

                            notificationType.Message.Replace("@1", "<em>" + collaborator.Name + "</em>"),
                            notificationType.MessageEnglish.Replace("@1", "<em>" + collaborator.Name + "</em>"),

                            "",
                            "",
                            "",
                            "https://hr-platform.s3.us-east-1.amazonaws.com/DefaultIconsDev/Bell.png",

                            false, // IsRead

                            true, // IsEditable
                            true, // IsDeleteable

                            collaborator.Id,
                            notificationType.Id,

                            creationDate,
                            editionDate
                        );

                        _notificationRepository.Add(notification);

                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }
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