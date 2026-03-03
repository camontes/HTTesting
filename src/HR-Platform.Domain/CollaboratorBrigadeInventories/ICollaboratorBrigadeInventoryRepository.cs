using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.CollaboratorBrigadeInventories;

public interface ICollaboratorBrigadeInventoryRepository
{
    Task<CollaboratorBrigadeInventory?> GetByIdAsync(CollaboratorBrigadeInventoryId id);
    Task<CollaboratorBrigadeInventory?> GetNoneCollaboratorBrigadeInventoryByCompanyIdAsync(CompanyId companyId);
    Task<List<CollaboratorBrigadeInventory>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<bool> ExistsAsync(CollaboratorBrigadeInventoryId id);
    void Add(CollaboratorBrigadeInventory pension);
    void AddRange(List<CollaboratorBrigadeInventory> CollaboratorBrigadeInventorys);
    void Update(CollaboratorBrigadeInventory CollaboratorBrigadeInventory);
    void Delete(CollaboratorBrigadeInventory CollaboratorBrigadeInventory);
}
