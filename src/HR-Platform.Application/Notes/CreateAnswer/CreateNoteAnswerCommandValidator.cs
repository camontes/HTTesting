using FluentValidation;

namespace HR_Platform.Application.Notes.CreateAnswer;

public class CreateNoteAnswerCommandValidator : AbstractValidator<CreateBaseNoteAnswerCommand>
{
    public CreateNoteAnswerCommandValidator()
    {
        RuleFor(r => r.Description)
            .MaximumLength(10000)
            .NotEmpty();

        RuleFor(r => r.MainNoteId)
            .NotEmpty();
    }
}
