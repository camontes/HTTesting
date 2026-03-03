using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.EmergencyPlans.Create;

public record CreateBaseEmergencyPlanCommand
(
    Guid EmergencyPlanTypeId,
    string Description,
    IFormFile? ImageFile,
    IFormFile? VideoFile
) : IRequest<ErrorOr<bool>>;


