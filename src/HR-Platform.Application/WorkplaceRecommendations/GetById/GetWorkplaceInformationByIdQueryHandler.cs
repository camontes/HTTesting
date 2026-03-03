using ErrorOr;
using HR_Platform.Application.WorkplaceRecommendations.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.WorkplaceRecommendations;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.WorkplaceRecommendations.GetById;

internal sealed class GetWorkplaceRecommendationByIdQueryHandler(
    IWorkplaceRecommendationRepository workplaceRecommendationRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference
    ) : IRequestHandler<GetWorkplaceRecommendationByIdQuery, ErrorOr<WorkplaceRecommendationFileResponse>>
{
    private readonly IWorkplaceRecommendationRepository _workplaceRecommendationRepository = workplaceRecommendationRepository ?? throw new ArgumentNullException(nameof(workplaceRecommendationRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<WorkplaceRecommendationFileResponse>> Handle(GetWorkplaceRecommendationByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _workplaceRecommendationRepository.GetByIdAsync(new WorkplaceRecommendationId(query.WorkplaceRecommendationId)) is not WorkplaceRecommendation oldWorkplaceRecommendation)
            return Error.NotFound("WorkplaceRecommendation.GetById", "The workplaceRecommendation with the provide id was not found");

        WorkplaceRecommendationFileResponse response = new
        (
            oldWorkplaceRecommendation.Id.Value, // IdFile
            oldWorkplaceRecommendation.FileName, // FileName
            oldWorkplaceRecommendation.UrlFile, // FileURL
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldWorkplaceRecommendation.CreationDate.Value).Split('.')[0]), // TimePosted
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldWorkplaceRecommendation.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
            oldWorkplaceRecommendation.CreationDate.Value, // CreationDate
            _timeFormatService.GetDateTimeFormatMonthToltip(oldWorkplaceRecommendation.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
            oldWorkplaceRecommendation.UrlPhotoWhoChangedByTH, // UrlPhotoTH
            oldWorkplaceRecommendation.NameWhoChangedByTH, // FullNameTh
            _stringService.GetInitials(oldWorkplaceRecommendation.NameWhoChangedByTH) // ShortNameTh
        );

        return response;
    }
}