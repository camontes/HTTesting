using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.ImprovementPlans.CreateResponses;

public record CreateBaseImprovementPlanResponsesCommand
(
    Guid ImprovementPlanId,

    List<IFormFile>? ImprovementPlanResponseFiles,

    string TaskResponsesJSON
)
:
IRequest<ErrorOr<bool>>;


public record ImprovementPlanResponsesRequest
(
    Guid ImprovementPlanTaskId,

    string ResponseDescription,

    List<string>? FileNames
);


