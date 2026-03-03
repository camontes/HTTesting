namespace HR_Platform.Application.Notifications.Common;

public record NotificationByCollaboratorResponse(
   Guid Id,
   string Message,
   string MessageEnglish,
   string ManageClaimDate,
   string ManageClaimDateEnglish,
   string ManageClaimDateToltip,
   string ImageURL,
   DateTime CreationDate,
   string CollaboratorId
);
