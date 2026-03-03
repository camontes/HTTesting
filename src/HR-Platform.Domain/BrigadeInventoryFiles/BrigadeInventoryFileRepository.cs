using HR_Platform.Domain.BrigadeInventories;

namespace HR_Platform.Domain.BrigadeInventoryFiles;

public interface IBrigadeInventoryFileRepository
{
    Task<List<BrigadeInventoryFile>> GetAll();
    Task<BrigadeInventoryFile?> GetByIdAsync(BrigadeInventoryFileId id);
    Task<List<BrigadeInventoryFile>?> GetByBrigadeInventoryIdAsync(BrigadeInventoryId inductionId);
    void AddRangeBrigadeInventoryFiles(List<BrigadeInventoryFile> BrigadeInventoryFile);
    void Add(BrigadeInventoryFile BrigadeInventoryFile);
    void Update(BrigadeInventoryFile BrigadeInventoryFile);
    void Delete(BrigadeInventoryFile BrigadeInventoryFile);
    void DeleteRange(List<BrigadeInventoryFile> tags);
}
