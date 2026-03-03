using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.DocumentManagements;

public interface IDocumentManagementRepository
{
    Task<DocumentManagement?> GetByIdAsync(DocumentManagementId id);
    Task<List<DocumentManagement>?> GetByCollaboratorIdAsync(CollaboratorId  collaboratorId);
    Task<bool> ExistsAsync(DocumentManagementId id);
    void Add(DocumentManagement DocumentManagement);
    void AddRange(List<DocumentManagement> DocumentManagements);
    void Update(DocumentManagement DocumentManagement);
    void Delete(DocumentManagement DocumentManagement);
    void DeleteRange(List<DocumentManagement> DocumentManagements);
}
