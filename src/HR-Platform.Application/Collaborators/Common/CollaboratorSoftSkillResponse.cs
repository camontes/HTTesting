namespace HR_Platform.Application.Collaborators.Common;

public record CollaboratorSoftSkillResponse
(
    string SoftSkillId,
    int? DefaultSoftSkillId,
    string? DefaultSoftSkill,
    string? DefaultSoftSkillEnglish,
    string? NameEnglish,
    string? OtherName
);