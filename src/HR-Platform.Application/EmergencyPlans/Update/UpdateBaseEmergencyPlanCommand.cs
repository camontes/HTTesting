using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.EmergencyPlans.Update;

public record UpdateBaseEmergencyPlanCommand
(
    Guid EmergencyPlanId,
    Guid EmergencyPlanTypeId,
    string Description,
    IFormFile? ImageFile,
    IFormFile? VideoFile,
    bool IsUpdateImageFile,
    bool IsUpdateVideoFile
) : IRequest<ErrorOr<bool>>;


