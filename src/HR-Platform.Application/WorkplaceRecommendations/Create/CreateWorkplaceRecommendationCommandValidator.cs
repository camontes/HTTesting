using FluentValidation;
using HR_Platform.Application.WorkplaceRecommendations.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateWorkplaceRecommendationCommandValidator : AbstractValidator<CreateBaseWorkplaceRecommendationCommand>
{
    public CreateWorkplaceRecommendationCommandValidator()
    {
        RuleFor(r => r.CollaboratorId)
            .NotEmpty();

        RuleFor(r => r.Files)
            .NotEmpty()
            .WithName("Files");
    }
}
