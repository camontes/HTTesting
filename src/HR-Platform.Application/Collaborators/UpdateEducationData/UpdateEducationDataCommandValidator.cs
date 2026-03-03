using FluentValidation;
using HR_Platform.Application.Collaborators.UpdateEducationData;

namespace HR_Platform.API.Application.Collaborators.Update;

public class UpdateEducationDataCommandValidator : AbstractValidator<UpdateEducationDataCommand>
{
    public UpdateEducationDataCommandValidator()
    {
        RuleFor(c => c.ProfessionalCard)
           .MaximumLength(50)
           .WithName("Professional Card");

    }
}
