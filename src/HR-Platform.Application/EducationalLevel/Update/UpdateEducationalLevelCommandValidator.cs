using FluentValidation;

namespace HR_Platform.Application.EducationalLevels.Update;

public class UpdateEducationalLevelCommandValidator : AbstractValidator<UpdateEducationalLevelCommand>
{
    public UpdateEducationalLevelCommandValidator()
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
