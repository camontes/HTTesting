using FluentValidation;
using HR_Platform.Application.Positions.Create;

namespace HR_Platform.Application.Pensions.Create;

public class CreatePositionsCommandValidator : AbstractValidator<CreatePositionsCommand>
{
    public CreatePositionsCommandValidator()
    {
        RuleFor(p => p.CompanyId)
            .NotEmpty();

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(p => p.NameEnglish)
            .NotEmpty()
            .MaximumLength(50)
            .WithName("Name English");

        RuleFor(p => p.Description)
            .MaximumLength(50);

        RuleFor(p => p.DescriptionEnglish)
            .MaximumLength(50)
            .WithName("Description English");

        RuleFor(p => p.IsEditable)
            .NotEmpty()
            .WithName("Is Editable");

        RuleFor(p => p.IsDeleteable)
            .NotEmpty()
            .WithName("Is Deleteable");
    }
}
