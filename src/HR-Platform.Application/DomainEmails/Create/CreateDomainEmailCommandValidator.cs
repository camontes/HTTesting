using FluentValidation;
using HR_Platform.Application.DomainEmails.Create;

namespace HR_Platform.API.Application.DomainEmails.Create;

public class CrerateDomainEmailCommandValidator : AbstractValidator<CreateDomainEmailCommand>
{
    public CrerateDomainEmailCommandValidator()
    {
        RuleFor(d => d.DomainEmail)
            .NotEmpty()
            .MaximumLength(100)
           .WithName("Domain Email");

        RuleFor(d => d.IsMainDomainEmail)
           .NotNull()
           .WithName("Is Main Domain Email");
    }
}
