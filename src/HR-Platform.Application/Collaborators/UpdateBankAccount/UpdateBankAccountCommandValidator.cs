using FluentValidation;

namespace Collaborators.UpdateBankAccount;

public class UpdateBankAccountCommandValidator : AbstractValidator<UpdateBaseBankAccountsCommand>
{
    public UpdateBankAccountCommandValidator()
    {
        RuleFor(r => r.TypeAccountId)
            .NotEmpty();

        RuleFor(r => r.BankId)
            .NotEmpty();

        RuleFor(r => r.accountNumberString)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(20);
    }
}
