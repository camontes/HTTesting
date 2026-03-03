using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DreamMapAnswers;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.CollaboratorDreamMapAnswers;

public interface ICollaboratorDreamMapAnswerRepository : ISearchFilterRepository<CollaboratorDreamMapAnswer>
{
    Task<List<CollaboratorDreamMapAnswer>> GetAll();
    Task<List<CollaboratorDreamMapAnswer>> GetAllCollaborators();
    Task<CollaboratorDreamMapAnswer?> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    Task<CollaboratorDreamMapAnswer?> GetByIdAsync(CollaboratorDreamMapAnswerId CollaboratorDreamMapAnswerId);
    void AddRangeCollaboratorDreamMapAnswers(List<CollaboratorDreamMapAnswer> CollaboratorDreamMapAnswer);
    void Add(CollaboratorDreamMapAnswer pension);
    void Update(CollaboratorDreamMapAnswer CollaboratorDreamMapAnswer);
    void UpdateRange(List<CollaboratorDreamMapAnswer> CollaboratorDreamMapAnswers);
    void Delete(CollaboratorDreamMapAnswer CollaboratorDreamMapAnswer);
    void DeleteRange(List<CollaboratorDreamMapAnswer> CollaboratorDreamMapAnswers);
}
