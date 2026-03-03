using ErrorOr;
using HR_Platform.Application.Pensions.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Pensions;
using MediatR;

namespace HR_Platform.Application.Pensions.GetByCompanyId;

internal sealed class GetPensionsByCompanyIdHandler(
    IPensionRepository PensionRepository,
    ICompanyRepository companyRepository
    ) : IRequestHandler<GetPensionsByCompanyIdQuery, ErrorOr<PensionsAndCountByCompanyResponse>>
{
    private readonly IPensionRepository _PensionRepository = PensionRepository ?? throw new ArgumentNullException(nameof(PensionRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<PensionsAndCountByCompanyResponse>> Handle(GetPensionsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }


        if (await _PensionRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), query.Page, query.PageSize) is not List<Pension> pensions)
        {
            return Error.NotFound("Pensions.NotFound", "The pensions related with the provide Company Id was not found.");
        }

        int pensionsCount = await _PensionRepository.GetNumberOfPensions(
            (new CompanyId(query.CompanyId)));

        List<PensionWIthCollaboratorCountResponse> pensionsResponse = [];


        if (pensions is not null && pensions.Count > 0)
        {
            foreach (Pension pension in pensions)
            {
                pensionsResponse.Add
                (
                    new PensionWIthCollaboratorCountResponse
                    (
                        pension.Id.Value,
                        pension.CompanyId.Value,

                        pension.Name,
                        pension.NameEnglish,

                        pension.Collaborators.Count,

                        pension.IsEditable,
                        pension.IsDeleteable,

                        pension.CreationDate.Value,
                        pension.EditionDate.Value
                    )
                );
            }
        }

        PensionsAndCountByCompanyResponse finalResult = new(
            pensionsResponse,
            pensionsCount
        );

        return finalResult;

    }
}