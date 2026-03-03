using FluentValidation;

namespace HR_Platform.Application.DreamMapAnswers.Create;

public class CreateDreamMapAnswerCommandValidator : AbstractValidator<DreamMapAnswerData>
{
    public CreateDreamMapAnswerCommandValidator()
    {
        RuleFor(r => r.Answer)
            .NotEmpty()
            .MaximumLength(500)
            .WithName("Answer");
    }
}
