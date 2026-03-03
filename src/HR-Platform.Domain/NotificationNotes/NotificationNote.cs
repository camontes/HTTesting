using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.NotificationNotes
{
    public sealed class NotificationNote : AggregateRoot
    {
        #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        private NotificationNote()
        {
        }

        public NotificationNote
        (
            NotificationNoteId id,

            bool isRead,

            bool isEditable,
            bool isDeleteable,

            CollaboratorId collaboratorId,

            TimeDate creationDate,
            TimeDate editionDate
        )
        {
            Id = id;


            IsRead = isRead;

            IsEditable = isEditable;
            IsDeleteable = isDeleteable;

            CollaboratorId = collaboratorId;

            CreationDate = creationDate;
            EditionDate = editionDate;
        }

        #pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        public NotificationNoteId Id { get; set; }

        public bool IsRead { get; set; }
        
        public bool IsEditable { get; set; }
        public bool IsDeleteable { get; set; }

        public CollaboratorId CollaboratorId { get; set; }
        public Collaborator Collaborator { get; set; }

        public TimeDate CreationDate { get; set; }
        public TimeDate EditionDate { get; set; }
    }
}
