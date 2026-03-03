using FluentValidation;
using HR_Platform.Application.CoexistenceCommitteeMinutes.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateCoexistenceCommitteeMinuteCommandValidator : AbstractValidator<CreateCoexistenceCommitteeMinutesCommand>
{
    public CreateCoexistenceCommitteeMinuteCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.CoexistenceCommitteeMinutesList)
            .NotEmpty();
    }
}
