using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.BrigadeAdjustments;

public interface IBrigadeAdjustmentRepository
{
    Task<List<BrigadeAdjustment>> GetAll();
    Task<BrigadeAdjustment?> GetByIdAsync(BrigadeAdjustmentId id);
    Task<BrigadeAdjustment?> GetNoneBrigadeAdjustmentByCompanyIdAsync(CompanyId companyId);
    Task<List<BrigadeAdjustment>?> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<bool> ExistsAsync(BrigadeAdjustmentId id);
    Task<int> GetNumberOfBrigadeAdjustments(CompanyId id);
    void AddRangeBrigadeAdjustments(List<BrigadeAdjustment> BrigadeAdjustment);
    void Add(BrigadeAdjustment pension);
    void Update(BrigadeAdjustment BrigadeAdjustment);
    void Delete(BrigadeAdjustment BrigadeAdjustment);
    void DeleteRange(List<BrigadeAdjustment> BrigadeAdjustments);
}
