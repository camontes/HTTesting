using ErrorOr;
using HR_Platform.Application.Notifications.Common;
using HR_Platform.Application.Notifications.GetAllClaimsByCompanyId;
using HR_Platform.Application.Services;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Notifications;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Notifications.GetAllByCollaboratorId;


internal sealed class GetAllByCollaboratorIdQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    ITimeElapsedFormatter timeElapsedFormatter,
    ITimeFormatService timeFormatService,
    INotificationRepository notificationRepository
    ) : IRequestHandler<GetAllByCollaboratorIdQuery, ErrorOr<NotificationResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ITimeElapsedFormatter _timeElapsedFormatter = timeElapsedFormatter ?? throw new ArgumentNullException(nameof(timeElapsedFormatter));
    private readonly INotificationRepository _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));

    public async Task<ErrorOr<NotificationResponse>> Handle(GetAllByCollaboratorIdQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.CollaboratorEmail) is not Collaborator oldCollaborator)
            return Error.Validation("Notification.CollaboratorId", "Collaborator with the provide email was not found");

        List<Notification> notifications = await _notificationRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);
        List<NotificationByCollaboratorResponse> Unread = [];
        List<NotificationByCollaboratorResponse> Read = [];

        if (notifications is not null && notifications.Count > 0)
        {
            foreach (Notification noty in notifications)
            {
                NotificationByCollaboratorResponse temp = new
                (
                    noty.Id.Value,
                    noty.Message,
                    noty.MessageEnglish,
                    _timeElapsedFormatter.GetTimeElapsed(noty.CreationDate.Value).Split(".")[0],
                    _timeElapsedFormatter.GetTimeElapsed(noty.CreationDate.Value).Split(".")[1],
                    _timeFormatService.GetDateTimeFormatMonthToltip(noty.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), //UpdateToltip
                    noty.SourcePhoto,
                    noty.CreationDate.Value,
                    noty.SourceName //  Use this field to save the Id of collaborator who is sending the answer in Notes
                );

                if (noty.IsRead)
                {
                    Read.Add(temp);
                }
                else
                {
                    Unread.Add(temp);
                }
            }
        }

        NotificationResponse response = new
        (
            [.. Unread.OrderByDescending(x => x.CreationDate)],
            [.. Read.OrderByDescending(x => x.CreationDate)]
        );

        return response;
    }
}