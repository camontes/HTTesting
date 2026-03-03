using HR_Platform.Domain.Roles;
using HR_Platform.Domain.RolesPermissions;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;

namespace HR_Platform.Infrastructure.PermissionsValidatorServices;

public class PermissionsValidatorService(
    IRolePermissionRepository rolePermissionRepository
    ) : IPermissionsValidatorService
{
    private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;

    public async Task<bool> IsValidatedPermission(RoleId roleId, string validationString)
    {
        List<RolePermission> rolePermissions = await _rolePermissionRepository.GetByRoleIdAsync(roleId);

        if (rolePermissions != null && rolePermissions.Count > 0)
        {
            foreach (RolePermission rolePermission in rolePermissions)
            {
                if (rolePermission.Permission.ValidationString == validationString)
                    return true;
            }
        }

        return false;
    }

    public async Task<Dictionary<string, bool>> IsValidatedPermissionsMultiple(RoleId roleId, List<string> validationStrings)
    {
        Dictionary<string, bool> dictionaryValidations = [];

        List<RolePermission> rolePermissions = await _rolePermissionRepository.GetByRoleIdAsync(roleId);

        if (validationStrings != null && validationStrings.Count > 0)
        {
            foreach (string validationString in validationStrings)
            {
                bool isAuth = false;

                if (rolePermissions != null && rolePermissions.Count > 0)
                {
                    foreach (RolePermission rolePermission in rolePermissions)
                    {
                        if (rolePermission.Permission.ValidationString == validationString)
                        {
                            isAuth = rolePermission.IsCheck;

                            break;
                        }
                    }
                }

                dictionaryValidations.TryAdd(validationString, isAuth);
            }
        }

        return dictionaryValidations;
    }
}
