namespace HR_Platform.Domain.ImprovementPlanTaskFiles;

public interface IImprovementPlanTaskFileRepository
{
    Task<ImprovementPlanTaskFile?> GetByIdAsync(ImprovementPlanTaskFileId id);
    Task<bool> ExistsAsync(ImprovementPlanTaskFileId id);
    void Add(ImprovementPlanTaskFile pension);
    void AddRange(List<ImprovementPlanTaskFile> ImprovementPlanFiles);
    void Update(ImprovementPlanTaskFile ImprovementPlanFile);
    void Delete(ImprovementPlanTaskFile ImprovementPlanFile);
}
