using ErrorOr;
using MediatR;

namespace HR_Platform.Application.Collaborators.CreateEducations;
public record UpdateEducationCommand
(
    string EmailChangeBy,

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

    string EducationFileURL,
    string EducationFileName

) : IRequest<ErrorOr<bool>>;
