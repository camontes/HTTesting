using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateSoftSkills;

public record UpdateBaseSoftSkillCommand
(
   Guid CollaboratorId,
   List<UpdateSoftSkillRequest> SoftSkillList
) : IRequest<ErrorOr<bool>>;

public record UpdateSoftSkillRequest
(
    string Id,
    string CollaboratorId,
    string? SoftSkillNameId,
    string? SoftSkillLevelId,
    string? OtherSoftSkillName
);




