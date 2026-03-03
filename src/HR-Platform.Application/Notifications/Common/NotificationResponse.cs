namespace HR_Platform.Application.Notifications.Common;

public record NotificationResponse(
   List<NotificationByCollaboratorResponse> Unread,
   List<NotificationByCollaboratorResponse> Read
);
