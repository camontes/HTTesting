using FluentValidation;
using HR_Platform.Application.WorkplaceEvidences.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateWorkplaceEvidenceCommandValidator : AbstractValidator<CreateBaseWorkplaceEvidenceCommand>
{
    public CreateWorkplaceEvidenceCommandValidator()
    {
        RuleFor(r => r.CollaboratorId)
            .NotEmpty();

        RuleFor(r => r.Files)
            .NotEmpty()
            .WithName("Files");
    }
}
