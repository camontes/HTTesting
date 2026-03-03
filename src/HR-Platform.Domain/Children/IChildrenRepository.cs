using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.ChildrenNamespace;

public interface IChildrenRepository
{
    Task<Children?> GetByIdAsync(ChildrenId id);
    Task<List<Children>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    void Add(Children children);
    void Update(Children children);
    void Delete(Children children);
}
