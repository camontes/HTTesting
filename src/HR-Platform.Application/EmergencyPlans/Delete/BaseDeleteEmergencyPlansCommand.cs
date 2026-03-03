using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.Delete;

public record BaseDeleteEmergencyPlansCommand(Guid EmergencyPlanId) : IRequest<ErrorOr<bool>>;

