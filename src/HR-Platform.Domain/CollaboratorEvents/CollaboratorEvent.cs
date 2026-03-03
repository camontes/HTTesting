using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Events;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.CollaboratorEvents;

public sealed class CollaboratorEvent : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private CollaboratorEvent()
    {
    }

    public CollaboratorEvent(CollaboratorEventId id, EventId eventId, CollaboratorId collaboratorId, bool notificationSent, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        EventId = eventId;
        CollaboratorId = collaboratorId;
        NotificationSent = notificationSent;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public CollaboratorEventId Id { get; set; }

    public EventId EventId { get; set; }
    public Event Event { get; set; }

    public CollaboratorId CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }
    public bool NotificationSent { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

