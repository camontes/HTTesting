using FluentValidation;

namespace HR_Platform.Application.Tags.Create;

public class CreateTypeAccountCommandValidator : AbstractValidator<TagData>
{
    public CreateTypeAccountCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
