using ErrorOr;
using HR_Platform.Application.Minutes.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Minutes;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Minutes.GetById;

internal sealed class GetByIdQueryHandler(
    IMinuteRepository minuteRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference
    ) : IRequestHandler<GetByIdQuery, ErrorOr<MinuteFileResponse>>
{
    private readonly IMinuteRepository _minuteRepository = minuteRepository ?? throw new ArgumentNullException(nameof(minuteRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<MinuteFileResponse>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _minuteRepository.GetByIdAsync(new MinuteId(query.MinuteId)) is not Minute oldMinute)
            return Error.NotFound("Minute.GetById", "The minute with the provide id was not found");

        MinuteFileResponse response = new
        (
            oldMinute.Id.Value, // IdFile
            oldMinute.FileName, // FileName
            oldMinute.UrlFile, // FileURL
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldMinute.CreationDate.Value).Split('.')[0]), // TimePosted
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldMinute.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
            oldMinute.CreationDate.Value, // CreationDate
            _timeFormatService.GetDateTimeFormatMonthToltip(oldMinute.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
            oldMinute.UrlPhotoWhoChangedByTH, // UrlPhotoTH
            oldMinute.NameWhoChangedByTH, // FullNameTh
            _stringService.GetInitials(oldMinute.NameWhoChangedByTH) // ShortNameTh
        );

        return response;
    }
}