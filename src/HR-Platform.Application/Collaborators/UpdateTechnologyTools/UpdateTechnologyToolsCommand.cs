using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateTechnologyTools;

public record UpdateTechnologyToolCommand
(
    string EmailChangeBy,
    Guid CollaboratorId,
    List<UpdateTechnologyToolRequest> TechnologyToolList
) : IRequest<ErrorOr<bool>>;






