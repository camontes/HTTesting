using ErrorOr;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
using HR_Platform.Application.Minutes.Common;
using HR_Platform.Domain.CoexistenceCommitteeMinutes;
using HR_Platform.Domain.Companies;
using MediatR;

namespace HR_Platform.Application.CoexistenceCommitteeMinutes.GetYearsByCompanyId;

internal sealed class GetYearsByCompanyIdQueryHandler(
    ICoexistenceCommitteeMinuteRepository coexistenceCommitteeMinuteRepository,
    ICompanyRepository companyRepository

    ) : IRequestHandler<GetYearsByCompanyIdQuery, ErrorOr<CoexistenceCommitteeMinuteYearsListResponse>>
{
    private readonly ICoexistenceCommitteeMinuteRepository _coexistenceCommitteeMinuteRepository =
        coexistenceCommitteeMinuteRepository ?? throw new ArgumentNullException(nameof(coexistenceCommitteeMinuteRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<CoexistenceCommitteeMinuteYearsListResponse>> Handle(GetYearsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<CoexistenceCommitteeMinute>? coexistenceCommitteeMinuteList = await _coexistenceCommitteeMinuteRepository.GetByCompanyIdAsync(oldCompany.Id);

        List<MinuteFileResponse> filesList = [];

        List<string> distinctYears = [];

        if (coexistenceCommitteeMinuteList is not null && coexistenceCommitteeMinuteList.Count > 0)
        {
            distinctYears =
                coexistenceCommitteeMinuteList
                .Select(m => m.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        CoexistenceCommitteeMinuteYearsListResponse response = new
        (
            distinctYears
        );

        return response;

    }
}
