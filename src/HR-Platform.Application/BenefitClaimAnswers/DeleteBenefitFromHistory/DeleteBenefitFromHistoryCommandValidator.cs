using BenefitClaimAnswers.DeleteBenefitFromHistory;
using FluentValidation;

namespace HR_Platform.Application.BenefitClaimAnswers.DeleteBenefitFromHistory;

public class DeleteBenefitFromHistoryCommandValidator : AbstractValidator<DeleteBaseBenefitFromHistoryCommand>
{
    public DeleteBenefitFromHistoryCommandValidator()
    {
        RuleFor(b => b.BenefitName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(b => b.Message)
            .NotEmpty()
            .MaximumLength(500);
    }
}
