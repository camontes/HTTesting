using FluentValidation;
using HR_Platform.Application.Positions.Update;

namespace HR_Platform.Application.Pensions.Update;

public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
{
    public UpdatePositionCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(p => p.Description)
            .MaximumLength(200);
    }
}
