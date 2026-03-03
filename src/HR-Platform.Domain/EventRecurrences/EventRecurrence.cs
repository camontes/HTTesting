using HR_Platform.Domain.DefaultEventReplays;
using HR_Platform.Domain.Events;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.EventRecurrences;

public sealed class EventRecurrence : AggregateRoot
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    private EventRecurrence()
    {
    }

    public EventRecurrence(EventRecurrenceId id, EventId eventId, DefaultEventReplayId eventReplayTypeId, int interval, TimeDate recurrenceEndDate, bool isEditable, bool isDeleteable, TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        EventId = eventId;
        EventReplayTypeId = eventReplayTypeId;

        Interval = interval;

        RecurrenceEndDate = recurrenceEndDate;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public EventRecurrenceId Id { get; set; }

    public EventId EventId { get; set; }
    public Event Event { get; set; }

    public DefaultEventReplayId EventReplayTypeId { get; set; }
    public DefaultEventReplay EventReplayType { get; set; }

    public int Interval { get; set; }

    public TimeDate RecurrenceEndDate { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

}

