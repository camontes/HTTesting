namespace HR_Platform.Domain.DefaultEventTypes;

public interface IDefaultEventTypeRepository
{
    Task<List<DefaultEventType>> GetAll();
    Task<bool> ExistsAsync(DefaultEventTypeId id);

}
