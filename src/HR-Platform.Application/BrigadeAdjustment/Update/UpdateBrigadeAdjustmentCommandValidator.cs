using FluentValidation;
using HR_Platform.Application.BrigadeAdjustments.Update;

namespace HR_Platform.Application.BrigadeAdjustments.Update;

public class UpdateBrigadeAdjustmentCommandValidator : AbstractValidator<UpdateBrigadeAdjustmentsCommand>
{
    public UpdateBrigadeAdjustmentCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .Matches(@"[^<>;/\\]+")
            .MaximumLength(50)
            .WithMessage("Name");


        RuleFor(r => r.BrigadeAdjustmentId)
            .NotEmpty();
    }
}
