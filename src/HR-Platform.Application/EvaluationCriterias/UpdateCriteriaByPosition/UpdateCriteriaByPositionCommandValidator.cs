using FluentValidation;

namespace HR_Platform.Application.EvaluationCriterias.UpdateCriteriaByPosition;

public class UpdateCriteriaByPositionCommandValidator : AbstractValidator<BaseCriterias>
{
    public UpdateCriteriaByPositionCommandValidator()
    {
        RuleFor(r => r.Name)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(r => r.Percentage)
           .GreaterThan(0)
           .LessThan(100);

        RuleFor(r => r.Description)
           .MaximumLength(200)
           .NotEmpty();
    }
}
