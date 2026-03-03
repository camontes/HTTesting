using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.BrigadeInventories;

public interface IBrigadeInventoryRepository : ISearchFilterRepository<BrigadeInventory>
{
    Task<BrigadeInventory?> GetByIdAsync(BrigadeInventoryId id);
    Task<BrigadeInventory?> GetNoneBrigadeInventoryByCompanyIdAsync(CompanyId companyId);
    Task<List<BrigadeInventory>?> GetByCompanyIdAsync(CompanyId CompanyId, string year);
    Task<bool> ExistsAsync(BrigadeInventoryId id);
    void Add(BrigadeInventory pension);
    void AddRange(List<BrigadeInventory> BrigadeInventorys);
    void Update(BrigadeInventory BrigadeInventory);
    void Delete(BrigadeInventory BrigadeInventory);
}
