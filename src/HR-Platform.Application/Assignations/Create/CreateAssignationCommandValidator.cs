using FluentValidation;
using HR_Platform.Application.Assignations.Create;

namespace HR_Platform.API.Application.Assignations.Create;

public class CreateAssignationCommandValidator : AbstractValidator<CreateAssignationElementCommand>
{
    public CreateAssignationCommandValidator()
    {
        RuleFor(j => j.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(j => j.NameEnglish)
            .NotEmpty()
            .MaximumLength(100)
            .WithName("Name English");

        RuleFor(j => j.IsEditable)
            .NotEmpty()
            .WithName("Is Editable");

        RuleFor(j => j.IsDeleteable)
            .NotEmpty()
            .WithName("Is Deleteable");

        RuleFor(j => j.IsInternalAssignation)
            .NotEmpty()
            .WithName("Is Internal Assignation");
    }
}
