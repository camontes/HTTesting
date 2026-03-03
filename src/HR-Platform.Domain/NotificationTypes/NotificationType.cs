using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.Primitives;

namespace HR_Platform.Domain.NotificationTypes
{
    public sealed class NotificationType : AggregateRoot
    {
        #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        private NotificationType()
        {
        }

        public NotificationType(NotificationTypeId id, string message, string messageEnglish)
        {
            Id = id;

            Message = message;
            MessageEnglish = messageEnglish;
        }

        #pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        public NotificationTypeId Id { get; set; }

        public string Message { get; set; } = string.Empty;
        public string MessageEnglish { get; set; } = string.Empty;

        public List<Notification> Notifications { get; set; }
    }
}
