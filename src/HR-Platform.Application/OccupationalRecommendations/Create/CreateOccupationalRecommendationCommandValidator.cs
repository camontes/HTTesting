using FluentValidation;
using HR_Platform.Application.OccupationalRecommendations.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateOccupationalRecommendationCommandValidator : AbstractValidator<CreateBaseOccupationalRecommendationCommand>
{
    public CreateOccupationalRecommendationCommandValidator()
    {
        RuleFor(r => r.CollaboratorId)
            .NotEmpty();

        RuleFor(r => r.Files)
            .NotEmpty()
            .WithName("Files");
    }
}
