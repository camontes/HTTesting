using FluentValidation;

namespace HR_Platform.Application.Collaborators.UpdateBasicInformation;

public class UpdateBasicInformationCommandValidator : AbstractValidator<UpdateBaseBasicInformationCommand>
{
    public UpdateBasicInformationCommandValidator()
    {
        //RuleFor(c => c.BusinessEmail)
        //    .EmailAddress()
        //    .MaximumLength(100)
        //   .WithName("Business Email");

        //RuleFor(c => c.Name)
        //    .MaximumLength(100);

        //RuleFor(c => c.Document)
        //   .MaximumLength(100);
    }
}
