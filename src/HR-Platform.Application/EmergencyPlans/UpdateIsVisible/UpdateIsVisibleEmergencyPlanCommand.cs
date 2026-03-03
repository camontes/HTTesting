using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.UpdateIsVisible;

public record UpdateIsVisibleEmergencyPlanCommand(Guid Id) : IRequest<ErrorOr<bool>>;