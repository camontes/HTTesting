using FluentValidation;
using HR_Platform.Application.Regulations.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateRegulationCommandValidator : AbstractValidator<CreateRegulationsCommand>
{
    public CreateRegulationCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.RegulationsList)
            .NotEmpty();
    }
}
