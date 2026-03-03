using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.Areas;

public interface IAreaRepository
{
    Task<List<Area>> GetAll();
    Task<List<Area>> GetByCompanyId(CompanyId companyId);
    Task<Area?> GetByIdAsync(AreaId areaId);
    void Update(Area area);
}
