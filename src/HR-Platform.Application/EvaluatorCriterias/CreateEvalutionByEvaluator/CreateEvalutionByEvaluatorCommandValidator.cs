using FluentValidation;

namespace HR_Platform.Application.EvaluatorCriterias.CreateEvalutionByEvaluator;

public class CreateEvalutionByEvaluatorCommandValidator : AbstractValidator<CreateBaseEvalutionByEvaluatorCommand>
{
    public CreateEvalutionByEvaluatorCommandValidator()
    {
        RuleFor(r => r.Comments)
            .MaximumLength(2000)
            .NotEmpty();

        RuleFor(r => r.PositionName)
           .NotEmpty();

        RuleFor(r => r.PositionNameEnglish)
           .NotEmpty();

        RuleFor(r => r.CriteriaAnswerList)
           .NotEmpty();
    }
}
