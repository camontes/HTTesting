namespace HR_Platform.Domain.DefaultAreas;

public interface IDefaultAreaRepository
{
    Task<List<DefaultArea>> GetAll();
    Task<DefaultArea?> GetByIdAsync(DefaultAreaId areaId);
}
