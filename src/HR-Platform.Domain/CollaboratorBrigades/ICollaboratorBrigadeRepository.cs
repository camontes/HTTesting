using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.CollaboratorBrigades;

public interface ICollaboratorBrigadeRepository : ISearchFilterRepository<CollaboratorBrigade>
{
    Task<List<CollaboratorBrigade>> GetAll();
    Task<CollaboratorBrigade?> GetByIdAsync(CollaboratorBrigadeId id);
    Task<List<CollaboratorBrigade>?> GetByCollaboratorBrigadeInventoryIdAsync(CollaboratorBrigadeInventoryId inductionId);
    Task<List<CollaboratorBrigade>> GetByBrigadeAdjustmentIdAsync(BrigadeAdjustmentId brigadeAdjustmentId);
    void AddRangeCollaboratorBrigades(List<CollaboratorBrigade> CollaboratorBrigade);
    void Add(CollaboratorBrigade CollaboratorBrigade);
    void Update(CollaboratorBrigade CollaboratorBrigade);
    void UpdateRange(List<CollaboratorBrigade> CollaboratorBrigades);
    void Delete(CollaboratorBrigade CollaboratorBrigade);
    void DeleteRange(List<CollaboratorBrigade> tags);
}
