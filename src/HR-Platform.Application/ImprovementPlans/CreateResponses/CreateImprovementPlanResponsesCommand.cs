using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ImprovementPlans.CreateResponses;

public record CreateImprovementPlanResponsesCommand
(
    Guid ImprovementPlanId,

    string EmailChangeBy,

    List<CreateImprovementPlanResponseObject> ImprovementPlanResponseObjects

)
:
IRequest<ErrorOr<bool>>;

public record CreateImprovementPlanResponseObject
(
    Guid ImprovementPlanTaskId,

    string ResponseDescription,

    List<CreateImprovementPlanResponseFiles> ImprovementPlanResponseFiles
);

public record CreateImprovementPlanResponseFiles
(
    string FileName,
    string UrlFile
);

