using FluentValidation;

namespace HR_Platform.Application.Roles.Create;

public class CrerateRoleCommandValidator : AbstractValidator<CreateRolesCommand>
{
    public CrerateRoleCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(r => r.NameEnglish)
            .NotEmpty()
            .MaximumLength(100)
            .WithName("Name English");

        RuleFor(r => r.IsEditable)
            .NotEmpty()
            .WithName("Is Editable");

        RuleFor(r => r.IsDeleteable)
            .NotEmpty()
            .WithName("Is Deleteable");
    }
}
