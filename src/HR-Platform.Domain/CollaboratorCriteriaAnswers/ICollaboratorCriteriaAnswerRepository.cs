using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.CollaboratorCriteriaAnswers;

public interface ICollaboratorCriteriaAnswerRepository
{
    Task<List<CollaboratorCriteriaAnswer>> GetAll();
    Task<List<CollaboratorCriteriaAnswer>> GetAllWithoutHistorical(CollaboratorId collaboratorId);
    Task<CollaboratorCriteriaAnswer?> GetByIdAsync(CollaboratorCriteriaAnswerId id);
    Task<List<CollaboratorCriteriaAnswer>?> GetByCollaboratorCriteriaAnswerIdAsync(CollaboratorCriteriaAnswerId inductionId);
    void AddRange(List<CollaboratorCriteriaAnswer> CollaboratorCriteriaAnswer);
    void Add(CollaboratorCriteriaAnswer CollaboratorCriteriaAnswer);
    void Update(CollaboratorCriteriaAnswer CollaboratorCriteriaAnswer);
    void UpdateRange(List<CollaboratorCriteriaAnswer> CollaboratorCriteriaAnswers);
    void Delete(CollaboratorCriteriaAnswer CollaboratorCriteriaAnswer);
}
