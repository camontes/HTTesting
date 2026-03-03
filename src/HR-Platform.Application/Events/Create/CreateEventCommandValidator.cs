using FluentValidation;

namespace HR_Platform.Application.Events.Create;

public class CreateEventCommandValidator : AbstractValidator<CreateBaseEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(r => r.EventName)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(r => r.StartDate)
            .NotEmpty();

        RuleFor(r => r.StartTime)
            .NotEmpty();

        RuleFor(r => r.EndDate)
            .NotEmpty();

        RuleFor(r => r.EndTime)
            .NotEmpty();

        RuleFor(r => r.EventTypeId)
            .NotEmpty();

        RuleFor(r => r.TimeZoneId)
            .NotEmpty();

        RuleFor(r => r.EventRecurrenceId)
            .NotEmpty();

        RuleFor(r => r.Description)
            .MaximumLength(1000)
            .NotEmpty();

        RuleFor(r => r.SendForAll)
            .NotEmpty();
    }
}
