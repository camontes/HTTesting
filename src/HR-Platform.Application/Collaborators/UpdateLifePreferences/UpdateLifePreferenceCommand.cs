using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateLifePreferences;

public record UpdateLifePreferenceCommand
(
   string EmailChangeBy,
   Guid CollaboratorId,
   List<UpdateLifePreferenceRequest> LifePreferenceList
) : IRequest<ErrorOr<bool>>;






