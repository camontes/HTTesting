using FluentValidation;

namespace HR_Platform.Application.Notes.Update;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateBaseNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(r => r.IsPublic)
            .NotEmpty();
    }
}
