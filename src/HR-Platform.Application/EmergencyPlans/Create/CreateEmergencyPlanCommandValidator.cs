using FluentValidation;
using HR_Platform.Application.EmergencyPlans.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateEmergencyPlanCommandValidator : AbstractValidator<CreateBaseEmergencyPlanCommand>
{
    public CreateEmergencyPlanCommandValidator()
    {
        RuleFor(r => r.EmergencyPlanTypeId)
            .NotEmpty()
            .WithName("EmergencyPlan Type Id");

        RuleFor(r => r.Description)
            .MaximumLength(1000)
            .NotEmpty()
            .WithName("Description");
    }
}
