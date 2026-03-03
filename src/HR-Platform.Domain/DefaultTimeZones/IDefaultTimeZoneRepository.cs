using HR_Platform.Domain.AssignationTypes;

namespace HR_Platform.Domain.DefaultTimeZones;

public interface IDefaultTimeZoneRepository
{
    Task<List<DefaultTimeZone>> GetAll();
    Task<bool> ExistsAsync(DefaultTimeZoneId id);

}
