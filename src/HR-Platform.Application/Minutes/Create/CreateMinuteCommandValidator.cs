using FluentValidation;
using HR_Platform.Application.Minutes.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateMinuteCommandValidator : AbstractValidator<CreateMinutesCommand>
{
    public CreateMinuteCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.MinutesList)
            .NotEmpty();
    }
}
