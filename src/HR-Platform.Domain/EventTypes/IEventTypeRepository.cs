using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.EventTypes;

public interface IEventTypeRepository
{
    Task<List<EventType>> GetAll();
    Task<EventType?> GetByIdAsync(EventTypeId id);
    Task<EventType?> GetNoneEventTypeByCompanyIdAsync(CompanyId companyId);
    Task<List<EventType>?> GetByCompanyIdAsync(CompanyId CompanyId);
    void AddRangeEventTypes(List<EventType> EventType);
    void Add(EventType pension);
    void Update(EventType EventType);
    void Delete(EventType EventType);
    void DeleteRange(List<EventType> EventTypes);
}
