using ErrorOr;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.NotificationNotes;
using HR_Platform.Domain.Primitives;
using MediatR;

namespace HR_Platform.Application.Notes.GetNotificationByCollaborator;

internal sealed class HideNotificationNoteQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    INotificationNoteRepository notificationNoteRepository,
    IUnitOfWork unitOfWork

    ) : IRequestHandler<HideNotificationNoteQuery, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly INotificationNoteRepository _notificationNoteRepository = notificationNoteRepository ?? throw new ArgumentNullException(nameof(notificationNoteRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(HideNotificationNoteQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.EmailWhoLogIn) is not Collaborator oldCollaborator)
            return Error.Validation("Notes.CollaboratorId", "The Collaborator with the provide Email wasn't found");

        List<NotificationNote> notifications = await _notificationNoteRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);

        if (notifications.Count > 0)
        {
            foreach (NotificationNote notification in notifications)
            {
                notification.IsRead = true;
            }
            _notificationNoteRepository.UpdateRange(notifications);
        }

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}