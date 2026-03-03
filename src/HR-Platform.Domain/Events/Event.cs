using HR_Platform.Domain.CollaboratorEvents;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DefaultTimeZones;
using HR_Platform.Domain.EventRecurrences;
using HR_Platform.Domain.EventTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Domain.Events;

public sealed class Event : AggregateRoot
{
    #pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    
    private Event()
    {
    }

    public Event(EventId id, CompanyId companyId, string name, TimeDate startDate, TimeSpan startTime, 
        TimeDate endDate, TimeSpan endTime, DefaultTimeZoneId timeZoneId, EventTypeId eventTypeId,
        string description, string emailCreatedBy, bool isDeletedEvent, bool isEditable, bool isDeleteable,
        TimeDate creationDate, TimeDate editionDate)
    {
        Id = id;

        CompanyId = companyId;

        Name = name;

        StartDate = startDate;
        StartTime = startTime;

        EndDate = endDate;
        EndTime = endTime;

        TimeZoneId = timeZoneId;

        EventTypeId = eventTypeId;

        Description = description;

        EmailCreatedBy = emailCreatedBy;

        IsDeletedEvent = isDeletedEvent;

        IsEditable = isEditable;
        IsDeleteable = isDeleteable;

        CreationDate = creationDate;
        EditionDate = editionDate;
    }

#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public EventId Id { get; set; }

    public CompanyId CompanyId { get; set; }
    public Company Company { get; set; }

    public string Name { get; set; } = string.Empty;

    public TimeDate StartDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeDate EndDate { get; set; }
    public TimeSpan EndTime { get; set; }

    public DefaultTimeZone TimeZone { get; set; }
    public DefaultTimeZoneId TimeZoneId { get; set; }

    public EventType EventType { get; set; }
    public EventTypeId EventTypeId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string EmailCreatedBy { get; set; } = string.Empty;

    public bool IsDeletedEvent { get; set; }

    public bool IsEditable { get; set; }
    public bool IsDeleteable { get; set; }

    public TimeDate CreationDate { get; set; }
    public TimeDate EditionDate { get; set; }

    public List<CollaboratorEvent> CollaboratorEvents { get; set; }
    public List<EventRecurrence> EventRecurrences { get; set; }

}

