using ErrorOr;
using HR_Platform.Application.OrganizationCharts.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.OrganizationCharts;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.OrganizationCharts.GetById;

internal sealed class GetOrganizationChartByIdQueryHandler(
    IOrganizationChartRepository organizationChartRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference
    ) : IRequestHandler<GetOrganizationChartByIdQuery, ErrorOr<OrganizationChartFileResponse>>
{
    private readonly IOrganizationChartRepository _organizationChartRepository = organizationChartRepository ?? throw new ArgumentNullException(nameof(organizationChartRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<OrganizationChartFileResponse>> Handle(GetOrganizationChartByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _organizationChartRepository.GetByIdAsync(new OrganizationChartId(query.OrganizationChartId)) is not OrganizationChart oldOrganizationChart)
            return Error.NotFound("OrganizationChart.GetById", "The OrganizationChart  with the provide id was not found");

        OrganizationChartFileResponse response = new
        (
               oldOrganizationChart.Id.Value, // IdFile
               oldOrganizationChart.IsByFile,
               oldOrganizationChart.IsByUrl,
               oldOrganizationChart.FileName, // FileName
               oldOrganizationChart.FileURL, // FileURL
               String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldOrganizationChart.FileCreatedDate.Value).Split('.')[0]), // TimePosted
               String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", oldOrganizationChart.FileCreatedDate.Value).Split('.')[1]), // TimePostedEnglish
               oldOrganizationChart.CreationDate.Value.ToString(), // CreationDate
               _timeFormatService.GetDateTimeFormatMonthToltip(oldOrganizationChart.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
               oldOrganizationChart.NameWhoChangedByTH, // FullNameTh
               _stringService.GetInitials(oldOrganizationChart.NameWhoChangedByTH) // ShortNameTh
        );

        return response;
    }
}