using ErrorOr;
using HR_Platform.Domain.CollaboratorEvents;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.DefaultEventReplays;
using HR_Platform.Domain.DefaultTimeZones;
using HR_Platform.Domain.EventRecurrences;
using HR_Platform.Domain.Events;
using HR_Platform.Domain.EventTypes;
using HR_Platform.Domain.Primitives;
using HR_Platform.Domain.ValueObjects;
using MediatR;

namespace HR_Platform.Application.Events.Create;

internal sealed class CreateEventCommandHandler(
    ICollaboratorRepository collaboratorRepository,
    ICollaboratorEventRepository collaboratorEventRepository,
    IEventRecurrenceRepository eventRecurrenceRepository,
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateEventCommand, ErrorOr<bool>>
{
    private readonly ICollaboratorRepository _collaboratorRepository = collaboratorRepository ?? throw new ArgumentNullException(nameof(collaboratorRepository));
    private readonly ICollaboratorEventRepository _collaboratorEventRepository = collaboratorEventRepository ?? throw new ArgumentNullException(nameof(collaboratorEventRepository));
    private readonly IEventRepository _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    private readonly IEventRecurrenceRepository _eventRecurrencetRepository = eventRecurrenceRepository ?? throw new ArgumentNullException(nameof(eventRecurrenceRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<ErrorOr<bool>> Handle(CreateEventCommand command, CancellationToken cancellationToken)
    {
        #region Date Formats
        DateTime horaColombiana = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SA Pacific Standard Time");
        string creationDateString = horaColombiana.ToString("MM/dd/yyyy HH:mm:ss");

        if (TimeDate.Create(creationDateString) is not TimeDate creationDate)
            return Error.Validation("Event.CreationDate", "CreationDate is not valid");

        string startDateString = command.StartDate.ToString("MM/dd/yyyy HH:mm:ss");
        if (TimeDate.Create(startDateString) is not TimeDate startDate)
            return Error.Validation("Event.StartDate", "StartDate is not valid");

        string endDateString = command.EndDate.ToString("MM/dd/yyyy HH:mm:ss");
        if (TimeDate.Create(endDateString) is not TimeDate endDate)
            return Error.Validation("Event.EndDate", "EndDate is not valid");

        string recurrenceEndDateString = DateTime.MinValue.ToString("MM/dd/yyyy HH:mm:ss");
        if (TimeDate.Create(recurrenceEndDateString) is not TimeDate recurrenceEndDate)
            return Error.Validation("Event.RecurrenceEndDate", "RecurrenceEndDate is not valid");
        #endregion

        #region Instances and calls
        List<CollaboratorEvent> collaboratorEvents = [];

        #endregion

        #region Validations
        //1 -Validate if the Collaborator has another event in his calendar
        if (await _collaboratorRepository.GetByEmailAsync(command.EmailCreateBy) is not Collaborator collaboratorWhoChanged)
            return Error.Validation("Event.FindCollaborator", "The collaborator with provide email was not found");
        #endregion

        #region Create Event
        Event requesEvent = new
        (
            new EventId(Guid.NewGuid()),
            new CompanyId(command.CompanyId),
            command.EventName,
            startDate,
            command.StartTime,
            endDate,
            command.EndTime,
            new DefaultTimeZoneId(command.TimeZoneId),
            new EventTypeId(command.EventTypeId),
            command.Description is not null ? command.Description : string.Empty,
            command.EmailCreateBy,
            false, //IsDeletedEvent
            true, //IsEditable
            true, //IsDeletable
            creationDate,
            creationDate
        );
        _eventRepository.Add(requesEvent);
        #endregion

        #region Add Event Recurrence
        EventRecurrence eventRecurrence = new
        (
            new EventRecurrenceId(Guid.NewGuid()),
            requesEvent.Id,
            new DefaultEventReplayId(command.EventRecurrenceId == 0 ? 1 : command.EventRecurrenceId),
            0, //Interval
            recurrenceEndDate,
            true, //IsEditable
            true, //IsDeletable
            creationDate,
            creationDate
        );
        _eventRecurrencetRepository.Add(eventRecurrence);
        #endregion

        #region Add Collaborator to Event
        ////CreateCollaboratorBrigadeInventoryCommandHandler 
        //Missing to add all collaborator option***
        CollaboratorEvent colEvent = new
        (
            new CollaboratorEventId(Guid.NewGuid()),
            requesEvent.Id,
            collaboratorWhoChanged.Id,
            false, //NotificationSent
            true, // IsEditable 
            true, // IsDeletable
            creationDate,
            creationDate
        );
        _collaboratorEventRepository.Add(colEvent);
        #endregion

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}