using HR_Platform.Domain.Companies;
using HR_Platform.Domain.RiskTypeMains;

namespace HR_Platform.Domain.Risks;

public interface IRiskRepository
{
    Task<Risk?> GetByIdAsync(RiskId id);
    Task<List<Risk>> GetRiskByRiskTypeIdAsync(RiskTypeMainId riskTypeId);
    Task<bool> ExistsAsync(RiskId id);
    void Add(Risk pension);
    void Update(Risk Risk);
    void Delete(Risk Risk);
}
