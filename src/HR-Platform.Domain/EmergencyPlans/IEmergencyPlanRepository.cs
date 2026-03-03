using HR_Platform.Domain.EmergencyPlanTypes;

namespace HR_Platform.Domain.EmergencyPlans;

public interface IEmergencyPlanRepository
{
    Task<List<EmergencyPlan>> GetAll();
    Task<EmergencyPlan?> GetByIdAsync(EmergencyPlanId id);
    Task<List<EmergencyPlan>> GetEmergencyPlanTypeId(EmergencyPlanTypeId EmergencyPlanTypeId);
    void Add(EmergencyPlan pension);
    void Update(EmergencyPlan EmergencyPlan);
    void Delete(EmergencyPlan EmergencyPlan);
}
