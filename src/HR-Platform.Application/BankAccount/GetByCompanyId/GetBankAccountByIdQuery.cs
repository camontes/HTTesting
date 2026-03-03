using ErrorOr;
using HR_Platform.Application.BankAccounts.Common;
using HR_Platform.Domain.BankAccounts;
using MediatR;

namespace HR_Platform.Application.BankAccounts.GetByCompanyId;

public record GetBankAccountByIdQuery(BankAccountId BankAccountId) : IRequest<ErrorOr<BankAccountsResponse>>;