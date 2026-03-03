namespace HR_Platform.Domain.DefaultEventReplays;

public interface IDefaultEventReplayRepository
{
    Task<List<DefaultEventReplay>> GetAll();
    Task<bool> ExistsAsync(DefaultEventReplayId id);

}
