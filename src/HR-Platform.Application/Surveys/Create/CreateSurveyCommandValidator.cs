using FluentValidation;
using HR_Platform.Domain.Surveys;

namespace HR_Platform.Application.Surveys.Create;

public class CreateSurveyCommandValidator : AbstractValidator<Survey>
{
    public CreateSurveyCommandValidator()
    {
        RuleFor(s => s.CompanyId)
            .NotEmpty();

        RuleFor(s => s.SurveyTypeId)
            .NotEmpty();

        RuleFor(s => s.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(s => s.Description)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(s => s.IsEditable)
            .NotEmpty()
            .WithName("Is Editable");

        RuleFor(s => s.IsDeleteable)
            .NotEmpty()
            .WithName("Is Deleteable");
    }
}
