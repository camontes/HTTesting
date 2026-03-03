using ErrorOr;
using MediatR;

namespace HR_Platform.Application.ContractTypes.Create;

public record CreateImprovementPlanCommand(
    string EmailChangeBy,
    Guid CollaboratorCriteriaAnswerId,
    List<CreateImprovementPlanObject> ImprovementPlanObjects
) : IRequest<ErrorOr<bool>>;

public record CreateImprovementPlanObject(
    string TaskDescription,
    List<CreateImprovementPlanFiles> ImprovementPlansFiles
);

public record CreateImprovementPlanFiles(
    string FileName,
    string UrlFile
);

