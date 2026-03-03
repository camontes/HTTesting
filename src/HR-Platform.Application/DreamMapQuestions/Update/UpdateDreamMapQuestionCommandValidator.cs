using FluentValidation;

namespace HR_Platform.Application.DreamMapQuestions.Update;

public class UpdateDreamMapQuestionCommandValidator : AbstractValidator<DreamMapQuestionUpdate>
{
    public UpdateDreamMapQuestionCommandValidator()
    {
        RuleFor(r => r.Question)
            .Matches(@"[^<>;/\\]+")
            .NotEmpty()
            .MaximumLength(200)
            .WithName("Question");
    }
}
