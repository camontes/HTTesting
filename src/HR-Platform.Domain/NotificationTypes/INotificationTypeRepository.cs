namespace HR_Platform.Domain.NotificationTypes;

public interface INotificationTypeRepository
{
    Task<NotificationType?> GetByIdAsync(NotificationTypeId id);
}
