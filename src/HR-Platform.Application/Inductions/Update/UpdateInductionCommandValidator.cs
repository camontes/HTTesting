using FluentValidation;
using HR_Platform.Application.Inductions.Update;

namespace HR_Platform.Application.ContractTypes.Update;

public class UpdateInductionCommandValidator : AbstractValidator<UpdateBaseInductionCommand>
{
    public UpdateInductionCommandValidator()
    {
        RuleFor(r => r.InductionId)
            .NotEmpty();

        RuleFor(r => r.Name)
            .Matches(@"[^<>;/\\]+") 
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(r => r.Description)
            .MaximumLength(5000)
            .NotEmpty();
    }
}
