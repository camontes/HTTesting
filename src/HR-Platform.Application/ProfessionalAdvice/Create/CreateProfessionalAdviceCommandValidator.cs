using FluentValidation;

namespace HR_Platform.Application.ProfessionalAdvices.Create;

public class CreateProfessionalAdviceCommandValidator : AbstractValidator<ProfessionalAdviceData>
{
    public CreateProfessionalAdviceCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.Name)
            .Matches(@"[^<>;/\\]+")
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(r => r.NameEnglish)
            .Matches(@"[^<>;/\\]+")
            .NotEmpty()
            .MaximumLength(100)
            .WithName("Name English");

        RuleFor(r => r.NameAcronyms)
            .NotEmpty()
            .MaximumLength(10)
            .WithName("Name Acronyms")
            .Matches("^[A-Za-z]*$"); 

        RuleFor(r => r.NameAcronymsEnglish)
            .NotEmpty()
            .MaximumLength(10)
            .WithName("Name English Acronyms")
            .Matches("^[A-Za-z]*$");

        RuleFor(r => r.IsEditable)
            .NotEmpty()
            .WithName("Is Editable");

        RuleFor(r => r.IsDeleteable)
            .NotEmpty()
            .WithName("Is Deleteable");
    }
}
