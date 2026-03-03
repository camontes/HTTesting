using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.NotificationTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.Notifications
{
    public sealed class Notification : AggregateRoot
    {
        #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        private Notification()
        {
        }

        public Notification
        (
            NotificationId id,
            
            string message,
            string messageEnglish,
            
            string sourceEmail,
            string sourceName,
            string sourceInitials,
            string sourcePhoto,

            bool isRead,

            bool isEditable,
            bool isDeleteable,

            CollaboratorId collaboratorId,

            NotificationTypeId notificationTypeId,
            
            TimeDate creationDate,
            TimeDate editionDate
        )
        {
            Id = id;

            Message = message;
            MessageEnglish = messageEnglish;

            SourceEmail = sourceEmail;
            SourceName = sourceName;
            SourceInitials = sourceInitials;
            SourcePhoto = sourcePhoto;

            IsRead = isRead;

            IsEditable = isEditable;
            IsDeleteable = isDeleteable;

            CollaboratorId = collaboratorId;
            NotificationTypeId = notificationTypeId;

            CreationDate = creationDate;
            EditionDate = editionDate;
        }

        #pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        public NotificationId Id { get; set; }

        public string Message { get; set; } = string.Empty;
        public string MessageEnglish { get; set; } = string.Empty;

        public string SourceEmail { get; set; } = string.Empty;
        public string SourceName { get; set; } = string.Empty;
        public string SourceInitials { get; set; } = string.Empty;
        public string SourcePhoto { get; set; } = string.Empty;

        public bool IsRead { get; set; }
        
        public bool IsEditable { get; set; }
        public bool IsDeleteable { get; set; }

        public CollaboratorId CollaboratorId { get; set; }
        public Collaborator Collaborator { get; set; }

        public NotificationTypeId NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }

        public TimeDate CreationDate { get; set; }
        public TimeDate EditionDate { get; set; }
    }
}
