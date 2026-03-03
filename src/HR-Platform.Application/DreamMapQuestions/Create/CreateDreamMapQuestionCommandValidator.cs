using FluentValidation;

namespace HR_Platform.Application.DreamMapQuestions.Create;

public class CreateDreamMapQuestionCommandValidator : AbstractValidator<DreamMapQuestionData>
{
    public CreateDreamMapQuestionCommandValidator()
    {
        RuleFor(r => r.Question)
            .Matches(@"[^<>;/\\]+")
            .NotEmpty()
            .MaximumLength(200)
            .WithName("Question");

        
    }
}
