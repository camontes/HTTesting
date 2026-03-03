using ErrorOr;
using HR_Platform.Domain.BankAccounts;
using MediatR;

namespace Collaborators.UpdateBankAccount;

public record UpdateBaseBankAccountsCommand(
    string CollaboratorId,
    string Id,
    string BankId,
    string TypeAccountId,
    string accountNumberString
) : IRequest<ErrorOr<bool>>;



