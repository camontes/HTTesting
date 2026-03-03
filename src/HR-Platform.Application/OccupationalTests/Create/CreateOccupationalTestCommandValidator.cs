using FluentValidation;
using HR_Platform.Application.OccupationalTests.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateOccupationalTestCommandValidator : AbstractValidator<CreateBaseOccupationalTestCommand>
{
    public CreateOccupationalTestCommandValidator()
    {
        RuleFor(r => r.CollaboratorId)
            .NotEmpty();

        RuleFor(r => r.Files)
            .NotEmpty()
            .WithName("Files");

        RuleFor(r => r.FileTypeIds)
            .NotEmpty()
            .WithName("File Type Ids");
    }
}
