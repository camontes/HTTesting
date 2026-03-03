using FluentValidation;
using HR_Platform.Application.EmergencyPlans.Update;

namespace HR_Platform.Application.ContractTypes.Update;

public class UpdateEmergencyPlanCommandValidator : AbstractValidator<UpdateBaseEmergencyPlanCommand>
{
    public UpdateEmergencyPlanCommandValidator()
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
