using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ImprovementPlans.Create;

public record CreateBaseImprovementPlanCommand
(
    Guid CollaboratorCriteriaAnswerId,
    List<IFormFile>? ImprovementPlanFiles,
    string TaskRequestsJson
) : IRequest<ErrorOr<bool>>;


public record TaskRequest
(
    string TaskDescription,
    List<string>? FileNames
);


