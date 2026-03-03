using HR_Platform.Domain.CollaboratorBrigadeInventories;

namespace HR_Platform.Domain.CollaboratorBrigadeInventoryFiles;

public interface ICollaboratorBrigadeInventoryFileRepository
{
    Task<List<CollaboratorBrigadeInventoryFile>> GetAll();
    Task<CollaboratorBrigadeInventoryFile?> GetByIdAsync(CollaboratorBrigadeInventoryFileId id);
    Task<List<CollaboratorBrigadeInventoryFile>?> GetByCollaboratorBrigadeInventoryIdAsync(CollaboratorBrigadeInventoryId inductionId);
    void AddRangeCollaboratorBrigadeInventoryFiles(List<CollaboratorBrigadeInventoryFile> CollaboratorBrigadeInventoryFile);
    void Add(CollaboratorBrigadeInventoryFile CollaboratorBrigadeInventoryFile);
    void Update(CollaboratorBrigadeInventoryFile CollaboratorBrigadeInventoryFile);
    void Delete(CollaboratorBrigadeInventoryFile CollaboratorBrigadeInventoryFile);
    void DeleteRange(List<CollaboratorBrigadeInventoryFile> tags);
}
