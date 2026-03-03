using ErrorOr;
using HR_Platform.Application.TypeAccounts.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.TypeAccounts;
using MediatR;

namespace HR_Platform.Application.TypeAccounts.GetByCompanyId;

internal sealed class GetTypeAccountsByCompanyIdHandler : IRequestHandler<GetTypeAccountsByCompanyIdQuery, ErrorOr<TypeAccountsAndCountByCompanyResponse>>
{
    private readonly ITypeAccountRepository _TypeAccountRepository;
    private readonly ICompanyRepository _companyRepository;

    public GetTypeAccountsByCompanyIdHandler
    (
        ITypeAccountRepository TypeAccountRepository,
        ICompanyRepository companyRepository
    )
    {
        _TypeAccountRepository = TypeAccountRepository ?? throw new ArgumentNullException(nameof(TypeAccountRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    }

    public async Task<ErrorOr<TypeAccountsAndCountByCompanyResponse>> Handle(GetTypeAccountsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }


        if (await _TypeAccountRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), query.Page, query.PageSize) is not List<TypeAccount> typeAccounts)
        {
            return Error.NotFound("TypeAccounts.NotFound", "The typeAccounts related with the provide Company Id was not found.");
        }

        int typeAccountsCount = await _TypeAccountRepository.GetNumberOfTypeAccounts(
            (new CompanyId(query.CompanyId)));

        List<TypeAccountWIthCollaboratorCountResponse> typeAccountsResponse = new();


        if (typeAccounts is not null && typeAccounts.Count > 0)
        {
            foreach (TypeAccount typeAccount in typeAccounts)
            {
                typeAccountsResponse.Add
                (
                    new TypeAccountWIthCollaboratorCountResponse
                    (
                        typeAccount.Id.Value,
                        typeAccount.CompanyId.Value,

                        typeAccount.Name,
                        typeAccount.NameEnglish,

                        typeAccount.BankAccounts.Count,

                        typeAccount.IsEditable,
                        typeAccount.IsDeleteable,

                        typeAccount.CreationDate.Value,
                        typeAccount.EditionDate.Value
                    )
                );
            }
        }

        TypeAccountsAndCountByCompanyResponse finalResult = new(
            typeAccountsResponse,
            typeAccountsCount
        );

        return finalResult;

    }
}