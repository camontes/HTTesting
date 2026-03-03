using HR_Platform.Domain.Inductions;

namespace HR_Platform.Domain.InductionFiles;

public interface IInductionFileRepository
{
    Task<List<InductionFile>> GetAll();
    Task<InductionFile?> GetByIdAsync(InductionFileId id);
    Task<List<InductionFile>?> GetByInductioIdAsync(InductionId inductionId);
    void AddRangeInductionFiles(List<InductionFile> InductionFile);
    void Add(InductionFile InductionFile);
    void Update(InductionFile InductionFile);
    void Delete(InductionFile InductionFile);
    void DeleteRange(List<InductionFile> tags);
}
