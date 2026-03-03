namespace HR_Platform.Application.Collaborators.Common;

public record CollaboratorsAndCountResponse(
    List<CollaboratorsResponse> Collaborators,
    
    int CollaboratorsCount
);
