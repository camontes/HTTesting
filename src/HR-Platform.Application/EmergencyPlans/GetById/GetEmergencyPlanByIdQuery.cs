using ErrorOr;
using HR_Platform.Application.EmergencyPlans.Common;
using MediatR;

namespace HR_Platform.Application.EmergencyPlans.GetById;

public record GetEmergencyPlanByIdQuery(Guid Id) : IRequest<ErrorOr<List<EmergencyPlanAllContentResponse>>>;