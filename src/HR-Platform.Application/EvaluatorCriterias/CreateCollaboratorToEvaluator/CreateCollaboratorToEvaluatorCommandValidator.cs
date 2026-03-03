using FluentValidation;

namespace HR_Platform.Application.EvaluatorCriterias.CreateCollaboratorToEvaluator;

public class CreateCollaboratorToEvaluatorCommandValidator : AbstractValidator<CreateCollaboratorToEvaluatorCommand>
{
    public CreateCollaboratorToEvaluatorCommandValidator()
    {
        RuleFor(r => r.PositionId)
            .NotEmpty();
    }
}
