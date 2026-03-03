using FluentValidation;
using HR_Platform.Application.NewCommunications.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateNewCommunicationCommandValidator : AbstractValidator<CreateBaseNewCommunicationCommand>
{
    public CreateNewCommunicationCommandValidator()
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
