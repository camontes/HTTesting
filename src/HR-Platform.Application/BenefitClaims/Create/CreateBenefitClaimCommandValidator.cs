using FluentValidation;

namespace HR_Platform.Application.BenefitClaims.Create;

public class CreateTypeAccountCommandValidator : AbstractValidator<CreateBenefitClaimsCommand>
{
    public CreateTypeAccountCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.BenefitId)
            .NotEmpty();

        RuleFor(r => r.CollaboratorEmail)
            .NotEmpty();
    }
}
