using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.CollaboratorLifePreferences;

public interface ICollaboratorLifePreferenceRepository
{
    Task<CollaboratorLifePreference?> GetByIdAsync(CollaboratorLifePreferenceId id);
    Task<List<CollaboratorLifePreference>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    void Add(CollaboratorLifePreference CollaboratorLifePreference);
    void AddRange(List<CollaboratorLifePreference> CollaboratorLifePreference);
    void DeleteRange(List<CollaboratorLifePreference> CollaboratorLifePreference);
    Task DeleteById(CollaboratorLifePreferenceId CollaboratorLifePreferenceId);
    void UpdateRange(List<CollaboratorLifePreference> CollaboratorLifePreference);
    void Update(CollaboratorLifePreference CollaboratorLifePreference);
    void Delete(CollaboratorLifePreference CollaboratorLifePreference);
}
