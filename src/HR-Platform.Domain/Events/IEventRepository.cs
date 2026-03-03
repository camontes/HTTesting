using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.Events;

public interface IEventRepository
{
    Task<List<Event>> GetAll();
    Task<Event?> GetByIdAsync(EventId id);
    Task<List<Event>?> GetByCompanyIdAsync(CompanyId CompanyId);
    void AddRangeEvents(List<Event> Event);
    void Add(Event pension);
    void Update(Event Event);
    void Delete(Event Event);
    void DeleteRange(List<Event> Events);
}
