using FluentValidation;

namespace HR_Platform.Application.ImprovementPlans.CreateResponses;

public class CreateImprovementPlanResponsesCommandValidator : AbstractValidator<ImprovementPlanResponsesRequest>
{
    public CreateImprovementPlanResponsesCommandValidator()
    {
        RuleFor(r => r.ResponseDescription)
            .MaximumLength(3000)
            .NotEmpty();
    }
}

