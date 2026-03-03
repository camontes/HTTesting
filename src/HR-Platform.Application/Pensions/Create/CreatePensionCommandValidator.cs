using FluentValidation;

namespace HR_Platform.Application.Pensions.Create;

public class CreateTypeAccountCommandValidator : AbstractValidator<PensionData>
{
    public CreateTypeAccountCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(r => r.NameEnglish)
            .NotEmpty()
            .MaximumLength(50)
            .WithName("Name English");

        RuleFor(r => r.IsEditable)
            .NotEmpty()
            .WithName("Is Editable");

        RuleFor(r => r.IsDeleteable)
            .NotEmpty()
            .WithName("Is Deleteable");
    }
}
