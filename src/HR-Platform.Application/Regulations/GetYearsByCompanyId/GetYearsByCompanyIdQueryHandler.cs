using ErrorOr;
using HR_Platform.Application.Minutes.Common;
using HR_Platform.Application.Regulations.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Regulations;
using MediatR;

namespace HR_Platform.Application.Regulations.GetYearsByCompanyId;

internal sealed class GetYearsByCompanyIdQueryHandler(
    IRegulationRepository regulationRepository,
    ICompanyRepository companyRepository

    ) : IRequestHandler<GetYearsByCompanyIdQuery, ErrorOr<RegulationFileYearsListResponse>>
{
    private readonly IRegulationRepository _regulationRepository =
        regulationRepository ?? throw new ArgumentNullException(nameof(regulationRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<RegulationFileYearsListResponse>> Handle(GetYearsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<Regulation>? regulationList = await _regulationRepository.GetByCompanyIdAsync(oldCompany.Id, string.Empty);

        List<MinuteFileResponse> filesList = [];

        List<string> distinctYears = [];

        if (regulationList is not null && regulationList.Count > 0)
        {
            distinctYears =
                regulationList
                .Select(r => r.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        RegulationFileYearsListResponse response = new
        (
            distinctYears
        );

        return response;

    }
}
