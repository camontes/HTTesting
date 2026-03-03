using FluentValidation;
using HR_Platform.Application.Risks.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateRiskCommandValidator : AbstractValidator<CreateBaseRiskCommand>
{
    public CreateRiskCommandValidator()
    {
        RuleFor(r => r.RiskTypeId)
            .NotEmpty()
            .WithName("Risk Type Id");

        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithName("Risk Name");

        RuleFor(r => r.Description)
            .MaximumLength(1000)
            .WithName("Description");
    }
}
