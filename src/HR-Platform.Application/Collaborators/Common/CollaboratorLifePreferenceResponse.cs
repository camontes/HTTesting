namespace HR_Platform.Application.Collaborators.Common;

public record CollaboratorLifePreferenceResponse
(
    string LifePreferenceId,
    int? DefaultLifePreferenceId,
    string? DefaultLifePreference,
    string? DefaultLifePreferenceEnglish,
    string? NameEnglish,
    string? OtherName
);