using ErrorOr;
using HR_Platform.Application.Banks.Common;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Banks;
using MediatR;

namespace HR_Platform.Application.Banks.GetByCompanyId;

internal sealed class GetBanksByCompanyIdHandler : IRequestHandler<GetBanksByCompanyIdQuery, ErrorOr<BanksAndCountByCompanyResponse>>
{
    private readonly IBankRepository _BankRepository;
    private readonly ICompanyRepository _companyRepository;

    public GetBanksByCompanyIdHandler
    (
        IBankRepository BankRepository,
        ICompanyRepository companyRepository
    )
    {
        _BankRepository = BankRepository ?? throw new ArgumentNullException(nameof(BankRepository));
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    }

    public async Task<ErrorOr<BanksAndCountByCompanyResponse>> Handle(GetBanksByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        if (await _companyRepository.GetByIdAsync(new CompanyId(query.CompanyId)) is null)
        {
            return Error.NotFound("Company.NotFound", "The company with the provide Id was not found.");
        }


        if (await _BankRepository.GetByCompanyIdAsync(new CompanyId(query.CompanyId), query.Page, query.PageSize) is not List<Bank> banks)
        {
            return Error.NotFound("Banks.NotFound", "The banks related with the provide Company Id was not found.");
        }

        int banksCount = await _BankRepository.GetNumberOfBanks(
            (new CompanyId(query.CompanyId)));

        List<BankWIthCollaboratorCountResponse> banksResponse = new();


        if (banks is not null && banks.Count > 0)
        {
            foreach (Bank bank in banks)
            {
                banksResponse.Add
                (
                    new BankWIthCollaboratorCountResponse
                    (
                        bank.Id.Value,
                        bank.CompanyId.Value,

                        bank.Name,
                        bank.NameEnglish,

                        bank.BankAccounts.Count,

                        bank.IsEditable,
                        bank.IsDeleteable,

                        bank.CreationDate.Value,
                        bank.EditionDate.Value
                    )
                );
            }
        }

        BanksAndCountByCompanyResponse finalResult = new(
            banksResponse,
            banksCount
        );

        return finalResult;

    }
}