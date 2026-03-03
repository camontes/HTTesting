using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Risks.Update;

public record UpdateBaseRiskCommand
(
    Guid RiskId,
    string Name,
    string? Description,
    IFormFile? ImageFile,
    IFormFile? VideoFile,
    bool IsUpdateImageFile,
    bool IsUpdateVideoFile
) : IRequest<ErrorOr<bool>>;


