using FluentValidation;
using HR_Platform.Application.Collaborators.Update;

namespace HR_Platform.Application.MasterUsers.Update;

public class UpdateMasterUserCommandValidator : AbstractValidator<UpdateBaseMasterUserCommand>
{
    public UpdateMasterUserCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .Matches(@"[^<>;/\\]+")
            .MaximumLength(100);


        RuleFor(c => c.Phone)
           .NotEmpty()
           .MaximumLength(50);
    }
}
