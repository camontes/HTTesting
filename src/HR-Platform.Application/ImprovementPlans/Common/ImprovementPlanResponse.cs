namespace HR_Platform.Application.ImprovementPlans.Common;
public record ImprovementPlanResponse
(
    Guid ImprovementPlanTaskId,

    string TaskDescription,

    List<ImprovementPlanFileResponse> ImprovementPlanFiles
);



