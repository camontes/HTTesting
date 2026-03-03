using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateLifePreferences;

public record UpdateBaseLifePreferenceCommand
(
   Guid CollaboratorId,
   List<UpdateLifePreferenceRequest> LifePreferenceList
) : IRequest<ErrorOr<bool>>;

public record UpdateLifePreferenceRequest
(
    string Id,
    string CollaboratorId,
    string? LifePreferenceNameId,
    string? LifePreferenceLevelId,
    string? OtherLifePreferenceName
);




