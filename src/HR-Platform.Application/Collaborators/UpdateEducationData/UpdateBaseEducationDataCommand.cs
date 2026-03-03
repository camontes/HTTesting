using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateEducationData;
public record UpdateBaseEducationDataCommand(
    Guid? Id,
    string? EducationLevelId,
    string? ProfessionalAdviceId,
    string? ProfessionalCard

) : IRequest<ErrorOr<UpdateCollaboratorResponse>>;
