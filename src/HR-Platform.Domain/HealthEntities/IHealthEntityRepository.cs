using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.HealthEntities;

public interface IHealthEntityRepository
{
    Task<List<HealthEntity>> GetAll();
    Task<HealthEntity?> GetByIdAsync(HealthEntityId id);
    Task<HealthEntity?> GetNoneHealthEntityByCompanyIdAsync(CompanyId companyId);
    Task<List<HealthEntity>?> GetByCompanyIdAsync(CompanyId CompanyId, int page, int pageSize);
    Task<bool> ExistsAsync(HealthEntityId id);
    void AddRange(List<HealthEntity> healthEntity);
    void Add(HealthEntity healthEntity);

    void Update(HealthEntity healthEntity);
    void Delete(HealthEntity healthEntity);
    void DeleteRange(List<HealthEntity> healthEntities);
}

