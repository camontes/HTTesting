namespace HR_Platform.Application.Collaborators.UpdateFamilyInformation;
public record UpdateChildrenCommand(
    string Id,

    string CollaboratorId,

    string Name,

    int Age
);