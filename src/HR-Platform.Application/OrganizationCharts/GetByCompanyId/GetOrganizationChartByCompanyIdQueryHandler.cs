using ErrorOr;
using HR_Platform.Application.OrganizationCharts.Common;
using HR_Platform.Application.OrganizationCharts.GetByCompanyId;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.OrganizationCharts;
using MediatR;
using System.Globalization;

namespace HR_Platform.Application.OrganizationCharts.GetByCollaboratorId;

internal sealed class GetOrganizationChartByCompanyIdQueryHandler(
    IOrganizationChartRepository organizationChartRepository,
    ICompanyRepository companyRepository,
    IStringService stringService,
    ITimeFormatService timeFormatService,
    ICalculateTimeDifference calculateTimeDifference

    ) : IRequestHandler<GetOrganizationChartByCompanyIdQuery, ErrorOr<OrganizationChartFileResponse?>>
{
    private readonly IOrganizationChartRepository _organizationChartRepository = organizationChartRepository ?? throw new ArgumentNullException(nameof(organizationChartRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IStringService _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
    private readonly ITimeFormatService _timeFormatService = timeFormatService ?? throw new ArgumentNullException(nameof(timeFormatService));
    private readonly ICalculateTimeDifference _calculateTimeDifference = calculateTimeDifference ?? throw new ArgumentNullException(nameof(calculateTimeDifference));

    public async Task<ErrorOr<OrganizationChartFileResponse?>> Handle(GetOrganizationChartByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        OrganizationChart? organizationChart = await _organizationChartRepository.GetByCompanyIdAsync(oldCompany.Id);
        OrganizationChartFileResponse? response = null;
        if (organizationChart is not null)
        {
            response = new
            (
               organizationChart.Id.Value, // IdFile
               organizationChart.IsByFile,
               organizationChart.IsByUrl,
               organizationChart.FileName, // FileName
               organizationChart.FileURL, // FileURL
               String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", organizationChart.FileCreatedDate.Value).Split('.')[0]), // TimePosted
               String.Join(" ", _calculateTimeDifference.CalculateTimeDifferenceFunction("Subido", "Uploaded", organizationChart.FileCreatedDate.Value).Split('.')[1]), // TimePostedEnglish
               organizationChart.CreationDate.Value.ToString(), // CreationDate
               _timeFormatService.GetDateTimeFormatMonthToltip(organizationChart.CreationDate.Value, "dd MMM yyyy HH:mm tt", new CultureInfo("es-CO")), // CreationDateTooltip,
               organizationChart.NameWhoChangedByTH, // FullNameTh
               _stringService.GetInitials(organizationChart.NameWhoChangedByTH) // ShortNameTh
            );
        }
        return response;
    }
}