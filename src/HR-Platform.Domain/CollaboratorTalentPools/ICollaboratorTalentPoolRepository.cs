using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.TalentPools;

namespace HR_Platform.Domain.CollaboratorTalentPools;

public interface ICollaboratorTalentPoolRepository
{
    Task<CollaboratorTalentPool?> GetByIdAsync(CollaboratorTalentPoolId id);
    Task<List<CollaboratorTalentPool>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    Task<bool> IsExistCollaboratorAsync(TalentPoolId talentPoolId, CollaboratorId collaboratorId);
    Task<List<CollaboratorTalentPool>> GetByTalentPoolIdAsync(TalentPoolId talentPoolId);
    void Add(CollaboratorTalentPool CollaboratorTalentPool);
    void AddRange(List<CollaboratorTalentPool> CollaboratorTalentPool);
    void DeleteRange(List<CollaboratorTalentPool> CollaboratorTalentPool);
    Task DeleteById(CollaboratorTalentPoolId CollaboratorTalentPoolId);
    void UpdateRange(List<CollaboratorTalentPool> CollaboratorTalentPool);
    void Update(CollaboratorTalentPool CollaboratorTalentPool);
    void Delete(CollaboratorTalentPool CollaboratorTalentPool);
}
