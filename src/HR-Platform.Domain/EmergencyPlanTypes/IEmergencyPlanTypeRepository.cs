using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.EmergencyPlanTypes;

public interface IEmergencyPlanTypeRepository
{
    Task<List<EmergencyPlanType>> GetAll();
    Task<EmergencyPlanType?> GetByIdAsync(EmergencyPlanTypeId id);
    Task<EmergencyPlanType?> GetNoneEmergencyPlanTypeByCompanyIdAsync(CompanyId companyId);
    Task<List<EmergencyPlanType>?> GetByCompanyIdAsync(CompanyId CompanyId);
    void Add(EmergencyPlanType pension);
    void Update(EmergencyPlanType EmergencyPlanType);
    void Delete(EmergencyPlanType EmergencyPlanType);
}
