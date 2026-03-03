using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.CollaboratorLanguages;

public interface ICollaboratorLanguageRepository
{
    Task<CollaboratorLanguage?> GetByIdAsync(CollaboratorLanguageId id);
    Task<List<CollaboratorLanguage>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId);
    void Add(CollaboratorLanguage CollaboratorLanguage);
    void AddRange(List<CollaboratorLanguage> CollaboratorLanguage);
    void DeleteRange(List<CollaboratorLanguage> CollaboratorLanguage);
    Task DeleteById(CollaboratorLanguageId collaboratorLanguageId);
    void UpdateRange(List<CollaboratorLanguage> CollaboratorLanguage);
    void Update(CollaboratorLanguage CollaboratorLanguage);
    void Delete(CollaboratorLanguage CollaboratorLanguage);
}
