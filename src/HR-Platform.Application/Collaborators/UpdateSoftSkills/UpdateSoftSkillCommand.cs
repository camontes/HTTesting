using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateSoftSkills;

public record UpdateSoftSkillCommand
(
    string EmailChangeBy,
    Guid CollaboratorId,
    List<UpdateSoftSkillRequest> SoftSkillList
) : IRequest<ErrorOr<bool>>;





