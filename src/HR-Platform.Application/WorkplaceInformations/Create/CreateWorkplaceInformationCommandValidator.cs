using FluentValidation;
using HR_Platform.Application.WorkplaceInformations.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateWorkplaceInformationCommandValidator : AbstractValidator<CreateBaseWorkplaceInformationCommand>
{
    public CreateWorkplaceInformationCommandValidator()
    {
        RuleFor(r => r.CollaboratorId)
            .NotEmpty();

        RuleFor(r => r.Files)
            .NotEmpty()
            .WithName("Files");
    }
}
