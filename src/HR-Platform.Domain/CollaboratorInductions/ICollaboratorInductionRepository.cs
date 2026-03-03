using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Inductions;

namespace HR_Platform.Domain.CollaboratorInductions;

public interface ICollaboratorInductionRepository
{
    Task<CollaboratorInduction?> GetByIdAsync(CollaboratorInductionId id);
    Task<List<CollaboratorInduction>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    Task<List<CollaboratorInduction>> GetByInductionIdAsync(InductionId inductionId);
    Task<List<CollaboratorInduction>> GetByCollaboratorAndInductionIdAsync(CollaboratorId collaboratorId, InductionId inductionId);
    Task<List<CollaboratorInduction>> GetAllAsync();
    void Add(CollaboratorInduction CollaboratorInduction);
    void AddRange(List<CollaboratorInduction> CollaboratorInduction);
    void DeleteRange(List<CollaboratorInduction> CollaboratorInduction);
    Task DeleteById(CollaboratorInductionId CollaboratorInductionId);
    void UpdateRange(List<CollaboratorInduction> CollaboratorInduction);
    void Update(CollaboratorInduction CollaboratorInduction);
    void Delete(CollaboratorInduction CollaboratorInduction);
}
