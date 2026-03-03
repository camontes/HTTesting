using ErrorOr;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.Delete;

public record DeleteEmergencyPlansCommand
(
    Guid EmergencyPlanId,
    Guid CompanyId
) : IRequest<ErrorOr<bool>>;

