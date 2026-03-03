using FluentValidation;

namespace HR_Platform.Application.ActiveBreaks.Update;

public class UpdateActiveBreakCommandValidator : AbstractValidator<UpdateActiveBreakCommand>
{
    public UpdateActiveBreakCommandValidator()
    {
        RuleFor(r => r.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(r => r.Description)
            .MaximumLength(2000)
            .NotEmpty();
    }
}

