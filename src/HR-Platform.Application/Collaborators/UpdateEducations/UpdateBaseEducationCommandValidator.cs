using FluentValidation;
using HR_Platform.Application.Collaborators.CreateEducations;

namespace HR_Platform.Application.Collaborators.UpdateBasicInformation;

public class UpdateBaseEducationCommandValidator : AbstractValidator<UpdateBaseEducationCommand>
{
    public UpdateBaseEducationCommandValidator()
    {
        RuleFor(c => c.InstitutionName)
            .NotEmpty()
            .MaximumLength(50)
            .WithName("Institution Name");

        RuleFor(c => c.ProfessionId)
            .NotEmpty()
            .WithName("Profession");

        RuleFor(c => c.OtherProfessionName)
            .MaximumLength(150);

        RuleFor(c => c.IsCompletedStudy)
            .NotEmpty()
            .WithName("Completed Study");

    }
}
