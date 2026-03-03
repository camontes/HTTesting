using ErrorOr;
using HR_Platform.Application.Collaborators.Common;
using MediatR;

namespace HR_Platform.Application.Collaborators.UpdateEducationData;
public record UpdateEducationDataCommand(
    string EmailChangeBy,
    Guid CollaboratorId,
    string CompanyId,
    string? EducationalLevelId,
    string? ProfessionalAdviceId,
    string? ProfessionalCard 

) : IRequest<ErrorOr<UpdateCollaboratorResponse>>;
