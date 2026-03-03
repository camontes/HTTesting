using FluentValidation;
using HR_Platform.Application.Risks.Update;

namespace HR_Platform.Application.ContractTypes.Update;

public class UpdateRiskCommandValidator : AbstractValidator<UpdateBaseRiskCommand>
{
    public UpdateRiskCommandValidator()
    {
        RuleFor(r => r.Name)
            .MaximumLength(100)
            .NotEmpty()
            .Matches(@"[^<>;/\\]+")
            .WithName("Name");

        RuleFor(r => r.Description)
            .MaximumLength(1000)
            .WithName("Description");
    }
}
