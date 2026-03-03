using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Tags;

namespace HR_Platform.Domain.CollaboratorTags;

public interface ICollaboratorTagRepository
{
    Task<CollaboratorTag?> GetByIdAsync(CollaboratorTagId id);
    Task<List<CollaboratorTag>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    Task<bool> IsExistCollaboratorAsync(TagId tagId, CollaboratorId collaboratorId);
    Task<bool> IsExistTagNameAsync(string tagName, CollaboratorId collaboratorId);
    void Add(CollaboratorTag CollaboratorTag);
    void AddRange(List<CollaboratorTag> CollaboratorTag);
    void DeleteRange(List<CollaboratorTag> CollaboratorTag);
    Task DeleteById(CollaboratorTagId collaboratorLanguageId);
    void UpdateRange(List<CollaboratorTag> CollaboratorTag);
    void Update(CollaboratorTag CollaboratorTag);
    void Delete(CollaboratorTag CollaboratorTag);
}
