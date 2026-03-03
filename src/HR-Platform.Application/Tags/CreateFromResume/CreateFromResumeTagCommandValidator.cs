using FluentValidation;

namespace HR_Platform.Application.Tags.CreateFromResume;

public class CreateFromResumeTypeAccountCommandValidator : AbstractValidator<CreateBaseFromResumeTagsCommand>
{
    public CreateFromResumeTypeAccountCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
