using HR_Platform.Domain.Collaborators;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Domain.Notifications;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(NotificationId Id);
    void Add(Notification notification);
    Task<List<Notification>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    Task<List<Notification>> GetAllReadByCollaboratorIdAsync(CollaboratorId collaboratorId);
    void Update(Notification notification);
    void Delete(Notification notification);
}
