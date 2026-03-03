using ErrorOr;
using HR_Platform.Application.OccupationalRecommendations.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.OccupationalRecommendations;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.OccupationalRecommendations.GetById;

internal sealed class GetOccupationalRecommendationByIdQueryHandler(
    IOccupationalRecommendationRepository minuteRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference
    ) : IRequestHandler<GetOccupationalRecommendationByIdQuery, ErrorOr<OccupationalRecommendationFileResponse>>
{
    private readonly IOccupationalRecommendationRepository _minuteRepository = minuteRepository ?? throw new ArgumentNullException(nameof(minuteRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<OccupationalRecommendationFileResponse>> Handle(GetOccupationalRecommendationByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _minuteRepository.GetByIdAsync(new OccupationalRecommendationId(query.OccupationalRecommendationId)) is not OccupationalRecommendation oldOccupationalRecommendation)
            return Error.NotFound("OccupationalRecommendation.GetById", "The minute with the provide id was not found");

        OccupationalRecommendationFileResponse response = new
        (
            oldOccupationalRecommendation.Id.Value, // IdFile
            oldOccupationalRecommendation.FileName, // FileName
            oldOccupationalRecommendation.UrlFile, // FileURL
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldOccupationalRecommendation.CreationDate.Value).Split('.')[0]), // TimePosted
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldOccupationalRecommendation.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
            oldOccupationalRecommendation.CreationDate.Value, // CreationDate
            _timeFormatService.GetDateTimeFormatMonthToltip(oldOccupationalRecommendation.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
            oldOccupationalRecommendation.UrlPhotoWhoChangedByTH, // UrlPhotoTH
            oldOccupationalRecommendation.NameWhoChangedByTH, // FullNameTh
            _stringService.GetInitials(oldOccupationalRecommendation.NameWhoChangedByTH) // ShortNameTh
        );

        return response;
    }
}