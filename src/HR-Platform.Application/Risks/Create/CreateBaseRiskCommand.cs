using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Risks.Create;

public record CreateBaseRiskCommand
(
    Guid RiskTypeId,
    string Name,
    string? Description,
    IFormFile? ImageFile,
    IFormFile? VideoFile
) : IRequest<ErrorOr<bool>>;


