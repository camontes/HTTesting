using FluentValidation;
using HR_Platform.Application.Forms.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateFormCommandValidator : AbstractValidator<CreateFormCommand>
{
    public CreateFormCommandValidator()
    {
        RuleFor(r => r.Name)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(r => r.Description)
            .MaximumLength(200)
            .NotEmpty();

        RuleForEach(x => x.QuestionTypeRequests).SetValidator(new QuestionTypeRequestValidator());
    }
    public class QuestionTypeRequestValidator : AbstractValidator<QuestionTypeRequest>
    {
        public QuestionTypeRequestValidator()
        {
            RuleFor(x => x.Question)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}