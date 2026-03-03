using ErrorOr;
using HR_Platform.Application.EmergencyPlans.Common;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.GetAllByEmergencyPlanType;

public record GetAllEmergencyPlansQuery(Guid CompanyId, bool IsVisible) : IRequest<ErrorOr<EmergencyPlanResponse>>;