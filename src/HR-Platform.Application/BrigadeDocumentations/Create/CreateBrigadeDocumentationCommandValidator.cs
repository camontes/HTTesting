using FluentValidation;

namespace HR_Platform.Application.BrigadeDocumentations.Create;

public class CreateBrigadeDocumentationCommandValidator : AbstractValidator<CreateBrigadeDocumentationsCommand>
{
    public CreateBrigadeDocumentationCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.BrigadeDocumentationsList)
            .NotEmpty();
    }
}
