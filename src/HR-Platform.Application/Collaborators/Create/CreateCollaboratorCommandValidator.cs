using FluentValidation;
using HR_Platform.Application.Collaborators.Create;

namespace HR_Platform.API.Application.Collaborators.Create;

public class CrerateCollaboratorCommandValidator : AbstractValidator<CreateCollaboratorsCommand>
{
    public CrerateCollaboratorCommandValidator()
    {
        RuleFor(c => c.BusinessEmail)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100)
           .WithName("Business Email");

        RuleFor(c => c.PersonalEmail)
            .EmailAddress()
            .MaximumLength(100)
           .WithName("Personal Email");

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.OtherDocumentType)
           .MaximumLength(50);

        //RuleFor(c => c.PhoneNumber)
        //   .MaximumLength(50)
        //   .WithName("Phone Number");

        //RuleFor(c => c.CellphoneNumber)
        //   .MaximumLength(50)
        //   .WithName("Cellphone Number");

        RuleFor(c => c.Document)
           .NotEmpty()
           .MaximumLength(100);

        RuleFor(c => c.StreetAddress)
           .MaximumLength(100)
           .WithName("Street Address");

        //RuleFor(c => c.Country)
        //   .MaximumLength(100);

        //RuleFor(c => c.State)
        //   .MaximumLength(100);

        //RuleFor(c => c.City)
        //   .MaximumLength(100);

        //RuleFor(c => c.ZipCode)
        //   .MaximumLength(100)
        //   .WithName("Zip Code");
    }
}
