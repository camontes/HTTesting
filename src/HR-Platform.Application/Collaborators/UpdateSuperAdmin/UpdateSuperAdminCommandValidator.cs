using FluentValidation;
using HR_Platform.Application.Collaborators.UpdateSuperAdmin;

namespace HR_Platform.Application.Collaborators.Update;

public class UpdateSuperAdminCommandValidator : AbstractValidator<UpdateBaseSuperAdminCommand>
{
    public UpdateSuperAdminCommandValidator()
    {
        RuleFor(c => c.Name)
            .Matches(@"[^<>;/\\]+")
            .MaximumLength(100);

        RuleFor(c => c.Phone)
           .NotEmpty()
           .MaximumLength(50);
    }
}
