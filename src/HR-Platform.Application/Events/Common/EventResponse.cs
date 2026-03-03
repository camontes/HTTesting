namespace HR_Platform.Application.Events.Common;

public record EventResponse(
    Guid EventId,
    string EventName,
    string EventDuration,
    string EventDurationEnglish,
    string EventStartDate,
    string EventStartTime,
    FormatDateToCalendarResponse FormatStartDate,
    string EventEndDate,
    string EventEndTime,
    FormatDateToCalendarResponse FormatEndDate,
    string EventType,
    string EventTypeEnglish,
    string EventDescription,
    int NumberEventAttendees,
    List<EventAttendeeResponse> EventAttendees
);
