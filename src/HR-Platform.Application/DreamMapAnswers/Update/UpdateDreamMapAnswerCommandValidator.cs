using FluentValidation;

namespace HR_Platform.Application.DreamMapAnswers.Update;

public class UpdateDreamMapAnswerCommandValidator : AbstractValidator<DreamMapAnswerUpdate>
{
    public UpdateDreamMapAnswerCommandValidator()
    {
        RuleFor(r => r.Answer)
            .NotEmpty()
            .MaximumLength(500)
            .WithName("Answer");
    }
}
