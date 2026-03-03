using FluentValidation;

namespace HR_Platform.Application.BirthdayTemplateSettings.Update;

public class CrerateBirthdayTemplateSettingCommandValidator : AbstractValidator<UpdateBirthdayTemplateSettingsCommand>
{
    public CrerateBirthdayTemplateSettingCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();
    }
}
