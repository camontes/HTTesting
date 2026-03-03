using ErrorOr;
using HR_Platform.Application.WorkplaceInformations.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.WorkplaceInformations;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.WorkplaceInformations.GetById;

internal sealed class GetWorkplaceInformationByIdQueryHandler(
    IWorkplaceInformationRepository minuteRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference
    ) : IRequestHandler<GetWorkplaceInformationByIdQuery, ErrorOr<WorkplaceInformationFileResponse>>
{
    private readonly IWorkplaceInformationRepository _minuteRepository = minuteRepository ?? throw new ArgumentNullException(nameof(minuteRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<WorkplaceInformationFileResponse>> Handle(GetWorkplaceInformationByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _minuteRepository.GetByIdAsync(new WorkplaceInformationId(query.WorkplaceInformationId)) is not WorkplaceInformation oldWorkplaceInformation)
            return Error.NotFound("WorkplaceInformation.GetById", "The minute with the provide id was not found");

        WorkplaceInformationFileResponse response = new
        (
            oldWorkplaceInformation.Id.Value, // IdFile
            oldWorkplaceInformation.FileName, // FileName
            oldWorkplaceInformation.UrlFile, // FileURL
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldWorkplaceInformation.CreationDate.Value).Split('.')[0]), // TimePosted
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldWorkplaceInformation.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
            oldWorkplaceInformation.CreationDate.Value, // CreationDate
            _timeFormatService.GetDateTimeFormatMonthToltip(oldWorkplaceInformation.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
            oldWorkplaceInformation.UrlPhotoWhoChangedByTH, // UrlPhotoTH
            oldWorkplaceInformation.NameWhoChangedByTH, // FullNameTh
            _stringService.GetInitials(oldWorkplaceInformation.NameWhoChangedByTH) // ShortNameTh
        );

        return response;
    }
}