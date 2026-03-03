using FluentValidation;
using HR_Platform.Application.NewCommunications.Update;

namespace HR_Platform.Application.ContractTypes.Update;

public class UpdateNewCommunicationCommandValidator : AbstractValidator<UpdateBaseNewCommunicationCommand>
{
    public UpdateNewCommunicationCommandValidator()
    {
        RuleFor(r => r.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(r => r.Description)
            .MaximumLength(2000)
            .NotEmpty();

        RuleFor(r => r.IsSurveyInclude)
            .NotEmpty();
    }
}
