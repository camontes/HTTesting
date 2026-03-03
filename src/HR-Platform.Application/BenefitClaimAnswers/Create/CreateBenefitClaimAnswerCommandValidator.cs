using FluentValidation;

namespace HR_Platform.Application.BenefitClaimAnswers.Create;

public class CreateBenefitClaimAnswersCommandValidator : AbstractValidator<CreateBaseBenefitClaimAnswersCommand>
{
    public CreateBenefitClaimAnswersCommandValidator()
    {
        RuleFor(b => b.BenefitClaimId)
            .NotEmpty();
    }
}
