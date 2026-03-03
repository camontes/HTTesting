using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.CollaboratorEvents;

public interface ICollaboratorEventRepository
{
    Task<List<CollaboratorEvent>> GetAll();
    Task<CollaboratorEvent?> GetByIdAsync(CollaboratorEventId id);
    Task<List<CollaboratorEvent>> GetByCollaboratorIdAsync(CollaboratorId id);
    void AddRangeCollaboratorEvents(List<CollaboratorEvent> CollaboratorEvent);
    void Add(CollaboratorEvent CollaboratorEvent);
    void AddRange(List<CollaboratorEvent> CollaboratorEvents);
    void Update(CollaboratorEvent CollaboratorEvent);
    void Delete(CollaboratorEvent CollaboratorEvent);
    void DeleteRange(List<CollaboratorEvent> tags);
}
