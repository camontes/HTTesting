using FluentValidation;

namespace Collaborators.UpdateContract;

public class UpdateContractCommandValidator : AbstractValidator<UpdateBaseContractsCommand>
{
    public UpdateContractCommandValidator()
    {
        RuleFor(r => r.PositionId)
            .NotEmpty();

        RuleFor(r => r.AssignationTypeId)
            .NotEmpty();

        RuleFor(r => r.AssignationId)
            .NotEmpty();

        RuleFor(r => r.Arl)
            .MaximumLength(200);

        RuleFor(r => r.Arl)
            .MaximumLength(1000);
    }
}
