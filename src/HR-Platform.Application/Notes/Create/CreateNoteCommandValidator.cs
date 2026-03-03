using FluentValidation;

namespace HR_Platform.Application.Notes.Create;
public class CreateNoteCommandValidator : AbstractValidator<CreateBaseNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(r => r.Description)
            .MaximumLength(10000)
            .NotEmpty();

        RuleFor(r => r.CollaboratorId)
            .NotEmpty();

        RuleFor(r => r.IsPublic)
            .NotEmpty();
    }
}
