using HR_Platform.Domain.Roles;

namespace HR_Platform.Infrastructure.ExternalServicesInterfaces;

public interface IPermissionsValidatorService
{
    Task<bool> IsValidatedPermission(RoleId roleId, string validationString);
    Task<Dictionary<string, bool>> IsValidatedPermissionsMultiple(RoleId roleId, List<string> validationStrings);
}
