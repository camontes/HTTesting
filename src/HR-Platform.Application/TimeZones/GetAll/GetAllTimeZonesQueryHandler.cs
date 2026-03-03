using ErrorOr;
using HR_Platform.Application.Collaborators.GetByCompanyIdAndIsPendingInvitation;
using HR_Platform.Application.TimeZones.Common;
using HR_Platform.Domain.DefaultTimeZones;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetAllTimeZonesQueryHandler(
    IDefaultTimeZoneRepository defaultTimeZoneRepository
    ) : IRequestHandler<GetAllTimeZonesQuery, ErrorOr<IReadOnlyList<TimeZonesResponse>>>
{
    private readonly IDefaultTimeZoneRepository _defaultTimeZoneRepository = defaultTimeZoneRepository ?? throw new ArgumentNullException(nameof(defaultTimeZoneRepository));

    public async Task<ErrorOr<IReadOnlyList<TimeZonesResponse>>> Handle(GetAllTimeZonesQuery query, CancellationToken cancellationToken)
    {
        IList<DefaultTimeZone> TimeZones = await _defaultTimeZoneRepository.GetAll();

        List<TimeZonesResponse> TimeZonesResponse = [];

        if (TimeZones is not null && TimeZones.Count > 0)
        {
            foreach (DefaultTimeZone assignationType in TimeZones)
            {
                TimeZonesResponse.Add
                (
                    new TimeZonesResponse
                    (
                        assignationType.Id.Value,

                        assignationType.Name,
                        assignationType.NameEnglish
                    )
                );
            }
        }
        
        return TimeZonesResponse;
    }
}