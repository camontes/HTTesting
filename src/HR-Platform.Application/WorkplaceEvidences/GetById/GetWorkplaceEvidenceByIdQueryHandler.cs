using ErrorOr;
using HR_Platform.Application.WorkplaceEvidences.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.WorkplaceEvidences;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.WorkplaceEvidences.GetById;

internal sealed class GetWorkplaceEvidenceByIdQueryHandler(
    IWorkplaceEvidenceRepository workplaceEvidenceRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference
    ) : IRequestHandler<GetWorkplaceEvidenceByIdQuery, ErrorOr<WorkplaceEvidenceFileResponse>>
{
    private readonly IWorkplaceEvidenceRepository _workplaceEvidenceRepository = workplaceEvidenceRepository ?? throw new ArgumentNullException(nameof(workplaceEvidenceRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<WorkplaceEvidenceFileResponse>> Handle(GetWorkplaceEvidenceByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _workplaceEvidenceRepository.GetByIdAsync(new WorkplaceEvidenceId(query.WorkplaceEvidenceId)) is not WorkplaceEvidence oldWorkplaceEvidence)
            return Error.NotFound("WorkplaceEvidence.GetById", "The workplaceEvidence with the provide id was not found");

        WorkplaceEvidenceFileResponse response = new
        (
            oldWorkplaceEvidence.Id.Value, // IdFile
            oldWorkplaceEvidence.FileName, // FileName
            oldWorkplaceEvidence.UrlFile, // FileURL
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldWorkplaceEvidence.CreationDate.Value).Split('.')[0]), // TimePosted
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldWorkplaceEvidence.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
            oldWorkplaceEvidence.CreationDate.Value, // CreationDate
            _timeFormatService.GetDateTimeFormatMonthToltip(oldWorkplaceEvidence.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
            oldWorkplaceEvidence.UrlPhotoWhoChangedByTH, // UrlPhotoTH
            oldWorkplaceEvidence.NameWhoChangedByTH, // FullNameTh
            _stringService.GetInitials(oldWorkplaceEvidence.NameWhoChangedByTH) // ShortNameTh
        );

        return response;
    }
}