using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
using HR_Platform.Application.CoexistenceCommitteeMinutes.GetById;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.CoexistenceCommitteeMinutes;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.CoexistenceCommitteeMinutes.GetCoexistenceCommitteeCoexistenceCommitteeMinuteById;

internal sealed class GetCoexistenceCommitteeCoexistenceCommitteeMinuteByIdQueryHandler(
    ICoexistenceCommitteeMinuteRepository minuteRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference
    ) : IRequestHandler<GetCoexistenceCommitteeMinuteByIdQuery, ErrorOr<CoexistenceCommitteeMinuteFileResponse>>
{
    private readonly ICoexistenceCommitteeMinuteRepository _minuteRepository = minuteRepository ?? throw new ArgumentNullException(nameof(minuteRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<CoexistenceCommitteeMinuteFileResponse>> Handle(GetCoexistenceCommitteeMinuteByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _minuteRepository.GetByIdAsync(new CoexistenceCommitteeMinuteId(query.CoexistenceCommitteeMinuteId)) is not CoexistenceCommitteeMinute oldCoexistenceCommitteeMinute)
            return Error.NotFound("CoexistenceCommitteeCoexistenceCommitteeMinute.GetCoexistenceCommitteeCoexistenceCommitteeMinuteById", "The minute with the provide id was not found");

        CoexistenceCommitteeMinuteFileResponse response = new
        (
            oldCoexistenceCommitteeMinute.Id.Value, // IdFile
            oldCoexistenceCommitteeMinute.FileName, // FileName
            oldCoexistenceCommitteeMinute.UrlFile, // FileURL
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldCoexistenceCommitteeMinute.CreationDate.Value).Split('.')[0]), // TimePosted
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldCoexistenceCommitteeMinute.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
            oldCoexistenceCommitteeMinute.CreationDate.Value, // CreationDate
            _timeFormatService.GetDateTimeFormatMonthToltip(oldCoexistenceCommitteeMinute.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
            oldCoexistenceCommitteeMinute.UrlPhotoWhoChangedByTH, // UrlPhotoTH
            oldCoexistenceCommitteeMinute.NameWhoChangedByTH, // FullNameTh
            _stringService.GetInitials(oldCoexistenceCommitteeMinute.NameWhoChangedByTH) // ShortNameTh
        );

        return response;
    }
}