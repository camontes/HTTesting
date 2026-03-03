namespace HR_Platform.Domain.DefaultRepeatEveryEvents;

public interface IDefaultRepeatEveryEventRepository
{
    Task<List<DefaultRepeatEveryEvent>> GetAll();
}
