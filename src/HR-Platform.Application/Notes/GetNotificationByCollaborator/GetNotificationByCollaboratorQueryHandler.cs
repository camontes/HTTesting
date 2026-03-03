using ErrorOr;
using HR_Platform.Application.Notes.Common;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.NotificationNotes;
using MediatR;

namespace HR_Platform.Application.Notes.GetNotificationByCollaborator;

internal sealed class GetNotificationByCollaboratorQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    INotificationNoteRepository notificationNoteRepository

    ) : IRequestHandler<GetNotificationByCollaboratorQuery, ErrorOr<NotificationNotesResponse>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly INotificationNoteRepository _notificationNoteRepository = notificationNoteRepository ?? throw new ArgumentNullException(nameof(notificationNoteRepository));

    public async Task<ErrorOr<NotificationNotesResponse>> Handle(GetNotificationByCollaboratorQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.EmailWhoLogIn) is not Collaborator oldCollaborator)
            return Error.Validation("Notes.CollaboratorId", "The Collaborator with the provide Email wasn't found");

        List<NotificationNote> notifications = await _notificationNoteRepository.GetByCollaboratorIdAsync(oldCollaborator.Id);

        int count = notifications.Count;
        NotificationNotesResponse response  = new
        (
            count > 0,
            count,
            $"There are {count} pending notifications"
        );

        return response;
    }
}