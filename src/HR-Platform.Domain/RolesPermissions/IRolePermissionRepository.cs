using HR_Platform.Domain.Roles;

namespace HR_Platform.Domain.RolesPermissions;

public interface IRolePermissionRepository
{
    Task<List<RolePermission>> GetByRoleAndPermissionIdAsync(RoleId roleId, int permissionId);
    Task<List<RolePermission>> GetByRoleIdAsync(RoleId roleId);
}
