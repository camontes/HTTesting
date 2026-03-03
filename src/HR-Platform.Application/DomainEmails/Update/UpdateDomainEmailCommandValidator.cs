using FluentValidation;
using HR_Platform.Application.DomainEmails.Create;
using HR_Platform.Application.DomainEmails.Update;

namespace HR_Platform.API.Application.DomainEmails.Create;

public class UpdateDomainEmailCommandValidator : AbstractValidator<UpdateDomainEmailCommand>
{
    public UpdateDomainEmailCommandValidator()
    {
        RuleFor(d => d.DomainEmail)
            .NotEmpty()
            .MaximumLength(100)
           .WithName("Domain Email");
    }
}
