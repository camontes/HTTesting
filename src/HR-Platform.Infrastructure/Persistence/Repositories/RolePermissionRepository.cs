using HR_Platform.Domain.Permissions;
using HR_Platform.Domain.Roles;
using HR_Platform.Domain.RolesPermissions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class RolePermissionRepository(ApplicationDbContext context) : IRolePermissionRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<RolePermission>> GetByRoleAndPermissionIdAsync(RoleId roleId, int permissionId) => await _context.RolesPermission
        .AsNoTracking()
        .Where(r => r.RoleId == roleId && r.PermissionId == new PermissionId(permissionId))
        .ToListAsync();

    public async Task<List<RolePermission>> GetByRoleIdAsync(RoleId roleId) => await _context.RolesPermission
        .AsNoTracking()
        .Where(r => r.RoleId == roleId)
        .Include(r => r.Permission)
        .ToListAsync();
}