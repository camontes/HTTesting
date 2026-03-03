using FluentValidation;

namespace HR_Platform.Application.Companies.Create;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100);

        RuleFor(c => c.CompanyName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.StreetAddress)
           .MaximumLength(100)
           .WithName("Street Address");

        RuleFor(c => c.Country)
           .MaximumLength(100);

        RuleFor(c => c.State)
           .MaximumLength(100);

        RuleFor(c => c.City)
           .MaximumLength(100);

        RuleFor(c => c.ZipCode)
           .MaximumLength(100)
           .WithName("Zip Code");

        RuleFor(c => c.PhoneNumber)
           .NotEmpty()
           .MaximumLength(50)
           .WithName("Phone Number");

        /**/

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
