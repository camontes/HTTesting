using ErrorOr;
using HR_Platform.Application.Regulations.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Regulations;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.Regulations.GetById;

internal sealed class GetRegulationByIdQueryHandler(
    IRegulationRepository regulationRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference
    ) : IRequestHandler<GetRegulationByIdQuery, ErrorOr<RegulationFileResponse>>
{
    private readonly IRegulationRepository _regulationRepository = regulationRepository ?? throw new ArgumentNullException(nameof(regulationRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<RegulationFileResponse>> Handle(GetRegulationByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _regulationRepository.GetByIdAsync(new RegulationId(query.RegulationId)) is not Regulation oldRegulation)
            return Error.NotFound("Regulation.GetRegulationById", "The regulation with the provide id was not found");

        RegulationFileResponse response = new
        (
            oldRegulation.Id.Value, // IdFile
            oldRegulation.FileName, // FileName
            oldRegulation.UrlFile, // FileURL
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldRegulation.CreationDate.Value).Split('.')[0]), // TimePosted
            String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldRegulation.CreationDate.Value).Split('.')[1]), // TimePostedEnglish
            oldRegulation.CreationDate.Value, // CreationDate
            _timeFormatService.GetDateTimeFormatMonthToltip(oldRegulation.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
            oldRegulation.UrlPhotoWhoChangedByTH, // UrlPhotoTH
            oldRegulation.NameWhoChangedByTH, // FullNameTh
            _stringService.GetInitials(oldRegulation.NameWhoChangedByTH) // ShortNameTh
        );

        return response;
    }
}