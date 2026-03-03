using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateTechnologyTools;

public record UpdateBaseTechnologyToolCommand
(
   Guid CollaboratorId,
   List<UpdateTechnologyToolRequest> TechnologyToolList
) : IRequest<ErrorOr<bool>>;

public record UpdateTechnologyToolRequest
(
    string Id,
    string CollaboratorId,
    string? TechnologyToolNameId,
    string? OtherKnowledgeLevelId,
    string? OtherTechnologyToolName,
    string? OtherKnowledgeLevelName

);




