namespace HR_Platform.Application.TalentPools.Common;

public record TalentPoolByCollaboratorIdResponse(
    Guid TalentPoolId,
    Guid CollaboratorTalentPoolId,
    string TalentPoolName
);

