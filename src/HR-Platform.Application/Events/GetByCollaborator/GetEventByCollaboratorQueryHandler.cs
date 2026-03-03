using ErrorOr;
using HR_Platform.Application.Events.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CollaboratorEvents;
using HR_Platform.Domain.Collaborators;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Events.GetByCollaborator;

internal sealed class GetEventByCollaboratorQueryHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorEventRepository collaboratorEventRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetEventByCollaboratorQuery, ErrorOr<List<EventResponse>>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorEventRepository _collaboratorEventRepository = collaboratorEventRepository ?? throw new ArgumentNullException(nameof(collaboratorEventRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<List<EventResponse>>> Handle(GetEventByCollaboratorQuery query, CancellationToken cancellationToken)
    {
        if (await _collaboratorRepository.GetByEmailAsync(query.CollaboratorEmail) is not Collaborator collaboratorWhoIsLogin)
            return Error.Validation("Event.FindCollaborator", "The collaborator with provide email was not found");

        List<CollaboratorEvent> collaboratorEvents = await _collaboratorEventRepository.GetByCollaboratorIdAsync(collaboratorWhoIsLogin.Id);
        List<EventResponse> eventResponse = [];

        if (collaboratorEvents.Count > 0)
        {
            foreach (CollaboratorEvent colEvent in collaboratorEvents)
            {
                List<EventAttendeeResponse> eventAttendees = [];

                List<CollaboratorEvent> attendeeList = colEvent.Event.CollaboratorEvents;
                int countAttendee = attendeeList.Count;

                if (countAttendee > 0)
                {
                    foreach (CollaboratorEvent attendee in colEvent.Event.CollaboratorEvents)
                    {
                        EventAttendeeResponse temp = new
                        (
                            attendee.Id.Value,
                            attendee.Collaborator.Name,
                            _stringService.GetInitials(attendee.Collaborator.Name),
                            attendee.Collaborator.Photo
                        );
                        eventAttendees.Add(temp);
                    }
                }

                DateTime combinedStartDateTime = colEvent.Event.StartDate.Value.Date.Add(colEvent.Event.StartTime);
                FormatDateToCalendarResponse formatStartDate = new
                (
                    combinedStartDateTime.Year.ToString(),
                    combinedStartDateTime.Month.ToString(),
                    combinedStartDateTime.Day.ToString(),
                    combinedStartDateTime.Hour.ToString(),
                    combinedStartDateTime.Minute.ToString()
                );

                DateTime combinedEndDateTime = colEvent.Event.EndDate.Value.Date.Add(colEvent.Event.EndTime);
                FormatDateToCalendarResponse formatEndDate = new
                (
                    combinedEndDateTime.Year.ToString(),
                    combinedEndDateTime.Month.ToString(),
                    combinedEndDateTime.Day.ToString(),
                    combinedEndDateTime.Hour.ToString(),
                    combinedEndDateTime.Minute.ToString()
                );

                EventResponse ev = new
                (
                    colEvent.EventId.Value,
                    colEvent.Event.Name,
                    _timeFormatService.GetDateTimeFormartForEvent(colEvent.Event.StartDate.Value, colEvent.Event.StartTime, colEvent.Event.EndDate.Value, colEvent.Event.EndTime, new CultureInfo("es-CO")),
                    _timeFormatService.GetDateTimeFormartForEvent(colEvent.Event.StartDate.Value, colEvent.Event.StartTime, colEvent.Event.EndDate.Value, colEvent.Event.EndTime, new CultureInfo("en-US")),
                    _timeFormatService.GetDateFormatMonthLarge(colEvent.Event.StartDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")),
                    colEvent.Event.StartTime.ToString(),
                    formatStartDate,
                     _timeFormatService.GetDateFormatMonthLarge(colEvent.Event.EndDate.Value, "dd MMMM yyyy", new CultureInfo("es-CO")),
                    colEvent.Event.EndTime.ToString(),
                    formatEndDate,
                    colEvent.Event.EventType.Name,
                    colEvent.Event.EventType.NameEnglish,
                    colEvent.Event.Description,
                    colEvent.Event.CollaboratorEvents.Count,
                    eventAttendees
                );
                eventResponse.Add(ev);
            }
        }

        return eventResponse;
    }
}