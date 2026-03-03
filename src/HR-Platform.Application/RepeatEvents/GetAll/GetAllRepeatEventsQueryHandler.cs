using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.RepeatEvents.Common;
using HR_Platform.Domain.DefaultDaysOfWeeks;
using HR_Platform.Domain.DefaultEventReplays;
using HR_Platform.Domain.DefaultMonths;
using HR_Platform.Domain.DefaultRepeatEveryEvents;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;
internal sealed class GetAllRepeatEventsQueryHandler(
    IDefaultMonthRepository defaultMonthRepository,
    IDefaultDaysOfWeekRepository defaultDaysOfWeekRepository,
    IDefaultRepeatEveryEventRepository defaultRepeatEveryEventRepository,
    IDefaultEventReplayRepository defaultEventReplayRepository
    ) : IRequestHandler<GetAllRepeatEventsQuery, ErrorOr<RepeatEventsResponse>>
{
    private readonly IDefaultMonthRepository _defaultMonthRepository = defaultMonthRepository ?? throw new ArgumentNullException(nameof(defaultMonthRepository));
    private readonly IDefaultDaysOfWeekRepository _defaultDaysOfWeekRepository = defaultDaysOfWeekRepository ?? throw new ArgumentNullException(nameof(defaultDaysOfWeekRepository));
    private readonly IDefaultRepeatEveryEventRepository _defaultRepeatEveryEventRepository = defaultRepeatEveryEventRepository ?? throw new ArgumentNullException(nameof(defaultRepeatEveryEventRepository));
    private readonly IDefaultEventReplayRepository _defaultEventReplayRepository = defaultEventReplayRepository ?? throw new ArgumentNullException(nameof(defaultEventReplayRepository));

    public async Task<ErrorOr<RepeatEventsResponse>> Handle(GetAllRepeatEventsQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultMonth> months = await _defaultMonthRepository.GetAll();
        IList<DefaultDaysOfWeek> daysOfWeek = await _defaultDaysOfWeekRepository.GetAll();
        IList<DefaultRepeatEveryEvent> repeatEvery = await _defaultRepeatEveryEventRepository.GetAll();
        IList<DefaultEventReplay> repeatType = await _defaultEventReplayRepository.GetAll();

        List<RepeatEventObjectResponse> monthsResponse = [];
        List<RepeatEventObjectResponse> daysOfWeekResponse = [];
        List<RepeatEventObjectResponse> repeatEveryResponse = [];
        List<RepeatEventObjectResponse> repeatTypResponse = [];

        if (months is not null && months.Count > 0)
        {
            foreach (DefaultMonth month in months)
            {
                monthsResponse.Add
                (
                    new RepeatEventObjectResponse
                    (
                        month.Id.Value,
                        month.Name,
                        month.NameEnglish
                    )
                );
            }
        }

        if (daysOfWeek is not null && daysOfWeek.Count > 0)
        {
            foreach (DefaultDaysOfWeek dayOfWeek in daysOfWeek)
            {
                daysOfWeekResponse.Add
                (
                    new RepeatEventObjectResponse
                    (
                        dayOfWeek.Id.Value,
                        dayOfWeek.Name,
                        dayOfWeek.NameEnglish
                    )
                );
            }
        }

        if (repeatEvery is not null && repeatEvery.Count > 0)
        {
            foreach (DefaultRepeatEveryEvent repeat in repeatEvery)
            {
                repeatEveryResponse.Add
                (
                    new RepeatEventObjectResponse
                    (
                        repeat.Id.Value,
                        repeat.Name,
                        repeat.NameEnglish
                    )
                );
            }
        }

        if (repeatType is not null && repeatType.Count > 0)
        {
            foreach (DefaultEventReplay type in repeatType)
            {
                repeatTypResponse.Add
                (
                    new RepeatEventObjectResponse
                    (
                        type.Id.Value,
                        type.Name,
                        type.NameEnglish
                    )
                );
            }
        }

        RepeatEventsResponse response = new
        (
            monthsResponse,
            daysOfWeekResponse,
            repeatEveryResponse,
            repeatTypResponse
        );

        return response;
    }
}