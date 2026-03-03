namespace HR_Platform.Domain.DefaultBrigadeAdjustments;

public interface IDefaultBrigadeAdjustmentRepository
{
    Task<List<DefaultBrigadeAdjustment>> GetAll();
}
