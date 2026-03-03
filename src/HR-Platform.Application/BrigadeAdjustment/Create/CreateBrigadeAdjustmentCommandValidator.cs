using FluentValidation;

namespace HR_Platform.Application.BrigadeAdjustments.Create;

public class CreateBrigadeAdjustmentCommandValidator : AbstractValidator<BrigadeAdjustmentData>
{
    public CreateBrigadeAdjustmentCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .Matches(@"[^<>;/\\]+")
            .MaximumLength(50)
            .WithMessage("Name");

        RuleFor(r => r.IconId)
            .NotEmpty();
    }
}
