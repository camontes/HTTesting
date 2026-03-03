using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Positions;

namespace HR_Platform.Domain.CollaboratorCriterias;

public interface ICollaboratorCriteriaRepository
{
    Task<List<CollaboratorCriteria>> GetAll();
    Task<CollaboratorCriteria?> GetByIdAsync(CollaboratorCriteriaId id);
    Task<List<CollaboratorCriteria>?> GetByCollaboratorCriteriaIdAsync(CollaboratorCriteriaId id);
    Task<List<CollaboratorCriteria>?> GetByCollaboratorIdAndEvaluatorIdAsync(CollaboratorId collaboratorId, CollaboratorId evaluatorId);
    Task<bool> IsExistingByCollaboratorIdAndEvaluatorIdAsync(CollaboratorId collaboratorId, CollaboratorId evaluatorId, PositionId positionId);
    Task<List<CollaboratorCriteria>?> GetByEvaluatorIdAsync(CollaboratorId id);
    void AddRange(List<CollaboratorCriteria> CollaboratorCriteria);
    void Add(CollaboratorCriteria CollaboratorCriteria);
    void Update(CollaboratorCriteria CollaboratorCriteria);
    void UpdateRange(List<CollaboratorCriteria> CollaboratorCriterias);
    void Delete(CollaboratorCriteria CollaboratorCriteria);
}
