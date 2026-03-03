using ErrorOr;
using HR_Platform.Application.BrigadeInventories.Common;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Common;
using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.Companies;
using MediatR;

namespace HR_Platform.Application.BrigadeInventories.GetYearsByCompanyId;

internal sealed class GetYearsByCompanyIdQueryHandler(
    IBrigadeInventoryRepository brigadeInventoryRepository,
    ICompanyRepository companyRepository

    ) : IRequestHandler<GetYearsByCompanyIdQuery, ErrorOr<BrigadeInventoryYearsListResponse>>
{
    private readonly IBrigadeInventoryRepository _brigadeInventoryRepository =
        brigadeInventoryRepository ?? throw new ArgumentNullException(nameof(brigadeInventoryRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<BrigadeInventoryYearsListResponse>> Handle(GetYearsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is not Company oldCompany)
            return Error.NotFound("Company.NotFound", "The Company with the provide Id was not found.");

        List<BrigadeInventory>? brigadeInventorysList =
            await _brigadeInventoryRepository.GetByCompanyIdAsync(oldCompany.Id, string.Empty);

        List<string> distinctYears = [];

        if (brigadeInventorysList is not null && brigadeInventorysList.Count > 0)
        {
            distinctYears =
                brigadeInventorysList
                .Select(m => m.CreationDate.Value.Year.ToString())
                .Distinct()
                .ToList();
        }

        distinctYears.Add(query.Language == 1 ? "Todos los años" : "All years");

        BrigadeInventoryYearsListResponse response = new
        (
            distinctYears
        );

        return response;

    }
}
