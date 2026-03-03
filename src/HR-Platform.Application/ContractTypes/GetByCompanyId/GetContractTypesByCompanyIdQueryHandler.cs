using ErrorOr;
using HR_Platform.Application.ContractTypes.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.ContractTypes;
using MediatR;

namespace HR_Platform.Application.ContractTypes.GetByCompanyId;

internal sealed class GetContractTypesByCompanyIdHandler(
    IContractTypeRepository ContractTypeRepository,
    ICompanyRepository companyRepository
    ) : IRequestHandler<GetContractTypesByCompanyIdQuery, ErrorOr<ContractTypesAndCountByCompanyResponse>>
{
    private readonly IContractTypeRepository _ContractTypeRepository = ContractTypeRepository ?? throw new ArgumentNullException(nameof(ContractTypeRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

    public async Task<ErrorOr<ContractTypesAndCountByCompanyResponse>> Handle(GetContractTypesByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }


        if (await _ContractTypeRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), query.Page, query.PageSize) is not List<ContractType> ContractTypes)
        {
            return Error.NotFound("ContractTypes.NotFound", "The ContractTypes related with the provide Company Id was not found.");
        }

        int ContractTypesCount = await _ContractTypeRepository.GetNumberOfContractTypes(
            (new CompanyId(query.CompanyId)));

        List<ContractTypeWIthCollaboratorCountResponse> ContractTypesResponse = [];


        if (ContractTypes is not null && ContractTypes.Count > 0)
        {
            foreach (ContractType ContractType in ContractTypes)
            {
                ContractTypesResponse.Add
                (
                    new ContractTypeWIthCollaboratorCountResponse
                    (
                        ContractType.Id.Value,
                        ContractType.CompanyId.Value,

                        ContractType.Name,
                        ContractType.NameEnglish,

                        ContractType.CollaboratorContracts.Count,

                        ContractType.IsEditable,
                        ContractType.IsDeleteable,

                        ContractType.CreationDate.Value,
                        ContractType.EditionDate.Value
                    )
                );
            }
        }

        ContractTypesAndCountByCompanyResponse finalResult = new(
            ContractTypesResponse,
            ContractTypesCount
        );

        return finalResult;

    }
}