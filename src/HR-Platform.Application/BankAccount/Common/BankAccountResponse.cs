namespace HR_Platform.Application.BankAccounts.Common;

public record BankAccountsResponse(
    string Id,
    string BankId,
    string TypeAccountId,
    string accountNumberString
);
