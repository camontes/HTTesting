using FluentValidation;

namespace HR_Platform.Application.ActiveBreaks.Create;

public class CreateBaseActiveBreakCommandValidator : AbstractValidator<CreateBaseActiveBreakCommand>
{
    public CreateBaseActiveBreakCommandValidator()
    {
        RuleFor(r => r.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(r => r.Description)
            .MaximumLength(2000)
            .NotEmpty();
    }
}
