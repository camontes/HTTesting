using ErrorOr;
using HR_Platform.Application.BankAccounts.Common;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Domain.BankAccounts;
using MediatR;

namespace HR_Platform.Application.BankAccounts.GetByCompanyId;

internal sealed class GetBankAccountByIdHandler(

        IBankAccountRepository BankAccountRepository,
        IEncryptService EncryptService

    ) : IRequestHandler<GetBankAccountByIdQuery, ErrorOr<BankAccountsResponse>>
{
    private readonly IBankAccountRepository _bankAccountRepository = BankAccountRepository ?? throw new ArgumentNullException(nameof(BankAccountRepository));
    private readonly IEncryptService _encryptService = EncryptService ?? throw new ArgumentNullException(nameof(EncryptService));

    public async Task<ErrorOr<BankAccountsResponse>> Handle(GetBankAccountByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _bankAccountRepository.GetByIdAsync(new BankAccountId(query.BankAccountId.Value)) is not BankAccount BankAccountById)
        {
            return Error.NotFound("Bank Account Id.NotFound", "The Bank Account Id with the provide Id was not found.");
        }

        string DecryptResult = string.Empty;

        if (BankAccountById.AccountNumber != null)
        {
            DecryptResult = _encryptService.DecryptString(BankAccountById.AccountNumber);   
        }

        BankAccountsResponse result = new(
         BankAccountById.Id is not null ? BankAccountById.Id.Value.ToString() : string.Empty,
         BankAccountById.TypeAccountId is not null ? BankAccountById.TypeAccountId.Value.ToString() : string.Empty,
         BankAccountById.BankId is not null ? BankAccountById.BankId.Value.ToString() : string.Empty,
         !string.IsNullOrEmpty(DecryptResult) ? DecryptResult : string.Empty
        );

        return result;

    }
}