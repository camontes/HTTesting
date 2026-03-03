using FluentValidation;
using HR_Platform.Application.DocumentManagements.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateDocumentManagementCommandValidator : AbstractValidator<CreateBaseDocumentManagementCommand>
{
    public CreateDocumentManagementCommandValidator()
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
