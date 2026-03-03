using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.NotificationNotes;

public interface INotificationNoteRepository
{
    Task<NotificationNote?> GetByIdAsync(NotificationNoteId Id);
    void Add(NotificationNote notification);
    Task<List<NotificationNote>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    Task<List<NotificationNote>> GetAllReadByCollaboratorIdAsync(CollaboratorId collaboratorId);
    void Update(NotificationNote notification);
    void UpdateRange(List<NotificationNote> notifications);
    void Delete(NotificationNote notification);
}
