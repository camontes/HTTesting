using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.Roles;

public interface IRoleRepository
{
    Task<List<Role>> GetAll();
    Task<Role?> GetByIdAsync(RoleId id);
    Task<Role?> GetByNameAsync(string nameRole);
    Task<Role?> GetByCompanyIdAndRoleNameAsync(CompanyId CompanyId, string roleName);
    Task<List<Role>> GetByCompanyIdAsync(CompanyId CompanyId);
    Task<List<Role>> GetByCompanyIdWithoutSuperAdminAsync(CompanyId companyId);
    Task<Role?> GetByCompanyIdSuperAdminAsync(CompanyId companyId);
    Task<bool> ExistsAsync(RoleId id);
    void Add(Role role);
    void Update(Role role);
    void Delete(Role role);
}
