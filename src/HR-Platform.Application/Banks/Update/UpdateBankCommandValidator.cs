using FluentValidation;

namespace HR_Platform.Application.Banks.Update;

public class UpdateBankCommandValidator : AbstractValidator<UpdateBankCommand>
{
    public UpdateBankCommandValidator()
    {
        RuleFor(j => j.Id)
           .NotEmpty();

        RuleFor(j => j.Name)
            .NotEmpty()
            .Matches(@"[^<>;/\\]+")
            .MaximumLength(50)
            .WithMessage("Name");

        RuleFor(j => j.NameEnglish)
            .NotEmpty()
            .MaximumLength(50)
            .WithName("Name English");
    }
}
