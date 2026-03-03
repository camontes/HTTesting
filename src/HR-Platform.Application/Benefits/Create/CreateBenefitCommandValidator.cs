using FluentValidation;
using HR_Platform.Application.Benefits.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateBenefitCommandValidator : AbstractValidator<CreateBaseBenefitCommand>
{
    public CreateBenefitCommandValidator()
    {
        RuleFor(r => r.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(r => r.Description)
            .MaximumLength(2000)
            .NotEmpty();

        RuleFor(r => r.IsAvailableForAll)
            .NotEmpty();

        RuleFor(r => r.AnotherContraint)
            .MaximumLength(50);

        RuleFor(r => r.IsAddedButton)
            .NotEmpty();

        RuleFor(r => r.ButtonName)
            .MaximumLength(10);

        RuleFor(r => r.IsSurveyInclude)
            .NotEmpty();
    }
}
