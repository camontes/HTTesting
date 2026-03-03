using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Events.Create;

public record CreateEventCommand
(
    Guid CompanyId,
    string EmailCreateBy,
    string EventName,
    DateTime StartDate,
    TimeSpan StartTime,
    DateTime EndDate,
    TimeSpan EndTime,
    Guid EventTypeId,
    int TimeZoneId,
    int EventRecurrenceId,
    string? Description,
    bool SendForAll,
    List<Guid>? CollaboratorIds
) : IRequest<ErrorOr<bool>>;

