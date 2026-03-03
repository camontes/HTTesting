using FluentValidation;
using HR_Platform.Application.ImprovementPlans.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateImprovementPlanCommandValidator : AbstractValidator<TaskRequest>
{
    public CreateImprovementPlanCommandValidator()
    {
        RuleFor(r => r.TaskDescription)
            .MaximumLength(3000)
            .NotEmpty();
    }
}
