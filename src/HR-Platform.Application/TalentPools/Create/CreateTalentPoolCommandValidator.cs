using FluentValidation;

namespace HR_Platform.Application.TalentPools.Create;

public class CreateTalentPoolCommandValidator : AbstractValidator<CreateTalentPoolsCommand>
{
    public CreateTalentPoolCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.Tittle)
            .Matches(@"[^<>;/\\]+")
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(r => r.Description)
            .MaximumLength(500)
            .WithName("Description");
    }
}
