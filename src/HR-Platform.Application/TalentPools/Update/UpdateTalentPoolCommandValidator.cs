using FluentValidation;

namespace HR_Platform.Application.TalentPools.Update;

public class UpdateTalentPoolCommandValidator : AbstractValidator<UpdateTalentPoolsCommand>
{
    public UpdateTalentPoolCommandValidator()
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
