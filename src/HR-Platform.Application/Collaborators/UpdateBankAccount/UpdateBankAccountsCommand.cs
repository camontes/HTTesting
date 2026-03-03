using ErrorOr;
using HR_Platform.Domain.BankAccounts;
using MediatR;

namespace Collaborators.UpdateBankAccount;

public record UpdateBankAccountsCommand(
    string EmailChangeBy,
    string BankAccountId,
    string CollaboratorId,
    string BankId,
    string TypeAccountId,
    string AccountNumber
) : IRequest<ErrorOr<bool>>;

