using FluentValidation;
using HR_Platform.Application.Inductions.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateInductionCommandValidator : AbstractValidator<CreateBaseInductionCommand>
{
    public CreateInductionCommandValidator()
    {
        RuleFor(r => r.Name)
            .Matches(@"[^<>;/\\]+") 
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(r => r.Description)
            .MaximumLength(5000)
            .NotEmpty();
    }
}
