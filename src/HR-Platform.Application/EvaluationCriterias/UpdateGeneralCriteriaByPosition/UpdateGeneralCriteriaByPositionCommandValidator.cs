using FluentValidation;

namespace HR_Platform.Application.EvaluationCriterias.UpdateGeneralCriteriaByPosition;

public class UpdateGeneralCriteriaByPositionCommandValidator : AbstractValidator<UpdateGeneralCriteriaByPositionCommand>
{
    public UpdateGeneralCriteriaByPositionCommandValidator()
    {
        RuleFor(r => r.SubjectiveCriteria)
          .GreaterThan(0)
          .LessThan(100);

        RuleFor(r => r.ObjectiveCriteria)
         .GreaterThan(0)
         .LessThan(100);
    }
}
