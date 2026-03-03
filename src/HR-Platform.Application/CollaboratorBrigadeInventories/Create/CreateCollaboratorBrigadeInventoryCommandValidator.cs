using FluentValidation;

namespace HR_Platform.Application.CollaboratorBrigadeInventories.Create;

public class CreateCollaboratorBrigadeInventoryCommandValidator : AbstractValidator<CreateBaseCollaboratorBrigadeInventoryCommand>
{
    public CreateCollaboratorBrigadeInventoryCommandValidator()
    {
        RuleFor(r => r.BrigadeMemberIdList)
            .NotNull();
        RuleFor(r => r.UnitMeasureId)
            .NotEmpty();

        RuleFor(r => r.Amount)
            .LessThan(1000)
            .GreaterThan(0);

        RuleFor(r => r.DeliveryDate)
            .NotEmpty();

        RuleFor(r => r.Observations)
            .MaximumLength(500);
    }
}
