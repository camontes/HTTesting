using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class RoleRepository(ApplicationDbContext context) : IRoleRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(Role role) => _context.Roles.Add(role);
    public void Delete(Role role) => _context.Roles.Remove(role);
    public void Update(Role role) => _context.Roles.Update(role);
    public async Task<bool> ExistsAsync(RoleId id) => await _context.Roles.AsNoTracking().AnyAsync(r => r.Id == id);
    public async Task<Role?> GetByIdAsync(RoleId id) => await _context.Roles.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);
    public async Task<Role?> GetByCompanyIdAndRoleNameAsync(CompanyId companyId, string roleName) =>
        await _context.Roles.AsNoTracking().SingleOrDefaultAsync(r => r.CompanyId == companyId && r.Name == roleName);
    public async Task<List<Role>> GetByCompanyIdAsync(CompanyId companyId) =>
        await _context.Roles.Where(r => r.CompanyId == companyId).ToListAsync();
    public async Task<List<Role>> GetByCompanyIdWithoutSuperAdminAsync(CompanyId companyId) =>
        await _context.Roles.Where(r => r.CompanyId == companyId && r.Name != "SuperAdministrador").ToListAsync();
    public async Task<List<Role>> GetAll() => await _context.Roles.AsNoTracking().ToListAsync();

    public async Task<Role?> GetByCompanyIdSuperAdminAsync(CompanyId companyId) =>
        await _context.Roles.SingleOrDefaultAsync(r => r.CompanyId == companyId && r.Name == "SuperAdministrador");

    public async Task<Role?> GetByNameAsync(string nameRole) => await _context.Roles.AsNoTracking().SingleOrDefaultAsync(r => r.Name == "Colaborador");
    
}