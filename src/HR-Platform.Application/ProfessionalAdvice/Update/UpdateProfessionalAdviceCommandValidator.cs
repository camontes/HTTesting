using FluentValidation;

namespace HR_Platform.Application.ProfessionalAdvices.Update;

public class UpdateProfessionalAdviceCommandValidator : AbstractValidator<UpdateProfessionalAdviceCommand>
{
    public UpdateProfessionalAdviceCommandValidator()
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

        RuleFor(r => r.NameAcronyms)
           .NotEmpty()
           .MaximumLength(10)
           .WithName("Name Acronyms")
           .Matches("^[A-Za-z]*$");
    }
}
