using FluentValidation;
using HR_Platform.Application.Collaborators.UpdateLocation;

namespace HR_Platform.API.Application.Collaborators.Update;

public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationCommandValidator()
    {
        RuleFor(c => c.LocationAddress)
           .MaximumLength(200)
           .WithName("Address");

         RuleFor(c => c.PhoneNumber)
           .MaximumLength(50)
           .WithName("Phone Number");

        RuleFor(c => c.PostalCode)
           .MaximumLength(50)
           .WithName("Postal Code");

    }
}
