namespace HR_Platform.Application.Collaborators.UpdateFamilyInformation;
public record UpdateFamilyCompositionCommand(
    string Id,

    string CollaboratorId,

    string Name,
    string NameEnglish
);