namespace HR_Platform.Application.Collaborators.Common;

public record CollaboratorEducationResponse
(
    string EducationId,

    string InstitutionName,

    int? ProfessionId,
    string? ProfessionName,
    string? ProfessionNameEnglish,
    string? OtherProfessionName,

    string? EducationLevelId,
    string? EducationLevelName,
    string? EducationLevelNameEnglish,

    int? StudyTypeId,
    string? StudyTypeName,
    string? StudyTypeNameEnglish,

    bool IsCertificated,

    int? StudyAreaId,
    string? StudyAreaName,
    string? StudyAreaNameEnglish,

    bool IsCompletedStudy,

    string? StartEducationDate,
    string? EndEducationDate,

    int? EducationStageId,
    string? EducationStageName,
    string? EducationStageNameEnglish,

    string? EducationFileName,
    string? EducationFileURL

);
