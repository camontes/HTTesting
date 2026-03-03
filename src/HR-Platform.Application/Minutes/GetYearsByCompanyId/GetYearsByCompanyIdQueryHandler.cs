using ErrorOr;
using HR_Platform.Application.Minutes.Common;
using HR_Platform.Application.Minutes.GetByCompanyId;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Minutes;
using MediatR;

namespace HR_Platform.Application.Minutes.GetYearsByCompanyId;

internal sealed class GetYearsByCompanyIdQueryHandler(
    IMinuteRepository minuteRepository,
    ICompanyRepository companyRepository

    ) : IRequestHandler<GetYearsByCompanyIdQuery, ErrorOr<MinuteYearsListResponse>>
{
    private readonly IMinuteRepository _minuteRepository = minuteRepository ?? throw new ArgumentNullException(nameof(minuteRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<MinuteYearsListResponse>> Handle(GetYearsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<Minute>? minuteList = await _minuteRepository.GetByCompanyIdAsync(oldCompany.Id);

        List<MinuteFileResponse> filesList = [];

        List<string> distinctYears = [];

        if (minuteList is not null && minuteList.Count > 0)
        {
            distinctYears =
                minuteList
                .Select(m => m.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        MinuteYearsListResponse response = new
        (
            distinctYears
        );

        return response;

    }
}