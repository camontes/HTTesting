using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Collaborators.CreateEducations;
public record UpdateBaseEducationCommand
(
    Guid CollaboratorId,

    string InstitutionName,

    int ProfessionId,

    string? OtherProfessionName,

    string? EducationLevelId,

    int StudyTypeId,

    bool IsCertificated,

    int StudyAreaId,

    bool IsCompletedStudy,

    DateTime? StartEducationDate,
    DateTime? EndEducationDate,

    int EducationStageId,

    IFormFile? EducationFile

) : IRequest<ErrorOr<bool>>;
