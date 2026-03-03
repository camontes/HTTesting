using ErrorOr;
using HR_Platform.Application.EventReplays.Common;
using HR_Platform.Domain.DefaultEventReplays;
using MediatR;

namespace HR_Platform.Application.EventReplays.GetAll;

internal sealed class GetAllEventReplaysQueryHandler(
    IDefaultEventReplayRepository defaultEventReplayRepository
    ) : IRequestHandler<GetAllEventReplaysQuery, ErrorOr<IReadOnlyList<EventReplaysResponse>>>
{
    private readonly IDefaultEventReplayRepository _defaultEventReplayRepository = defaultEventReplayRepository ?? throw new ArgumentNullException(nameof(defaultEventReplayRepository));

    public async Task<ErrorOr<IReadOnlyList<EventReplaysResponse>>> Handle(GetAllEventReplaysQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultEventReplay> EventReplays = await _defaultEventReplayRepository.GetAll();

        List<EventReplaysResponse> EventReplaysResponse = [];

        if (EventReplays is not null && EventReplays.Count > 0)
        {
            foreach (DefaultEventReplay eventReplay in EventReplays)
            {
                EventReplaysResponse.Add
                (
                    new EventReplaysResponse
                    (
                        eventReplay.Id.Value,

                        eventReplay.Name,
                        eventReplay.NameEnglish
                    )
                );
            }
        }
        
        return EventReplaysResponse;
    }
}