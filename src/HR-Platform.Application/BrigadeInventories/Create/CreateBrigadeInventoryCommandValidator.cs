using FluentValidation;
using HR_Platform.Application.BrigadeInventories.Create;

namespace HR_Platform.Application.ContractTypes.Create;

public class CreateBrigadeInventoryCommandValidator : AbstractValidator<CreateBaseBrigadeInventoryCommand>
{
    public CreateBrigadeInventoryCommandValidator()
    {
        RuleFor(r => r.Name)
            .Matches(@"[^<>;/\\]+")
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(r => r.Description)
            .MaximumLength(500)
            .NotEmpty();

        RuleFor(r => r.CompanyLocation)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(r => r.Other)
           .MaximumLength(50);

        RuleFor(r => r.Amount)
            .LessThan(1000);

        RuleFor(r => r.UnitMeasureId)
            .NotEmpty();

        RuleFor(r => r.ApplyPurchaseDate)
            .NotEmpty();

        RuleFor(r => r.ApplyExpirationDate)
            .NotEmpty(); 

        RuleFor(r => r.Observations)
            .MaximumLength(500);
    }
}
