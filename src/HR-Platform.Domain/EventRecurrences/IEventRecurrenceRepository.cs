namespace HR_Platform.Domain.EventRecurrences;

public interface IEventRecurrenceRepository
{
    Task<List<EventRecurrence>> GetAll();
    Task<EventRecurrence?> GetByIdAsync(EventRecurrenceId id);
    void AddRangeEventRecurrences(List<EventRecurrence> EventRecurrence);
    void Add(EventRecurrence EventRecurrence);
    void Update(EventRecurrence EventRecurrence);
    void Delete(EventRecurrence EventRecurrence);
    void DeleteRange(List<EventRecurrence> tags);
}
