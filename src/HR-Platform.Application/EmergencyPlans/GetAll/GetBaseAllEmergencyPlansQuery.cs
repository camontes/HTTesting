using ErrorOr;
using HR_Platform.Application.EmergencyPlans.Common;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.GetAllByEmergencyPlanType;

public record GetBaseAllEmergencyPlansQuery(bool IsVisible) : IRequest<ErrorOr<List<EmergencyPlanResponse>>>;