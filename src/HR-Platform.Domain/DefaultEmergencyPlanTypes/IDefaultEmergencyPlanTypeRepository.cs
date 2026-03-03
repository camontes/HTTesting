namespace HR_Platform.Domain.DefaultEmergencyPlanTypes;

public interface IDefaultEmergencyPlanTypeRepository
{
    Task<List<DefaultEmergencyPlanType>> GetAll();
}
