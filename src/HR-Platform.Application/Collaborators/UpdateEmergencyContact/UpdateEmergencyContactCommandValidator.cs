using FluentValidation;
using HR_Platform.Application.Collaborators.UpdateEducationData;

namespace HR_Platform.API.Application.Collaborators.Update;

public class UpdateEmergencyContactCommandValidator : AbstractValidator<UpdateEmergencyContactCommand>
{
    public UpdateEmergencyContactCommandValidator()
    {
        RuleFor(c => c.ContactName)
           .MaximumLength(100)
           .WithName("Contact Name");

        RuleFor(c => c.Relationship)
           .MaximumLength(100)
           .WithName("Relationship");

        RuleFor(c => c.PhoneNumber)
           .MaximumLength(50)
           .WithName("Phone Numner");

        RuleFor(c => c.Address)
           .MaximumLength(200)
           .WithName("Contact Name");
    }
}
