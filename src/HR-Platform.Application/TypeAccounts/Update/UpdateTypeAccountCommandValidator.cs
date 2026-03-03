using FluentValidation;

namespace HR_Platform.Application.TypeAccounts.Update;

public class UpdateTypeAccountCommandValidator : AbstractValidator<UpdateTypeAccountCommand>
{
    public UpdateTypeAccountCommandValidator()
    {
        RuleFor(j => j.Id)
           .NotEmpty();

        RuleFor(j => j.Name)
            .NotEmpty()
            .Matches(@"[^<>;/\\]+")
            .MaximumLength(100)
            .WithMessage("Name");

        RuleFor(j => j.NameEnglish)
            .NotEmpty()
            .MaximumLength(100)
            .WithName("Name English");
    }
}
