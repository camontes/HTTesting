using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.EventTypes.Common;
using HR_Platform.Domain.EventTypes;
using MediatR;

namespace HR_Platform.Application.EventTypes.GetAll;

internal sealed class GetAllEventTypesQueryHandler(
    IEventTypeRepository eventTypeRepository
    ) : IRequestHandler<GetAllEventTypesQuery, ErrorOr<IReadOnlyList<EventTypesResponse>>>
{
    private readonly IEventTypeRepository _eventTypeRepository = eventTypeRepository ?? throw new ArgumentNullException(nameof(eventTypeRepository));

    public async Task<ErrorOr<IReadOnlyList<EventTypesResponse>>> Handle(GetAllEventTypesQuery query, CancellationToken cancellationToken)
    {
        IList<EventType> EventTypes = await _eventTypeRepository.GetAll();

        List<EventTypesResponse> EventTypesResponse = [];

        if (EventTypes is not null && EventTypes.Count > 0)
        {
            foreach (EventType eventType in EventTypes)
            {
                EventTypesResponse.Add
                (
                    new EventTypesResponse
                    (
                        eventType.Id.Value,

                        eventType.Name,
                        eventType.NameEnglish
                    )
                );
            }
        }
        
        return EventTypesResponse;
    }
}