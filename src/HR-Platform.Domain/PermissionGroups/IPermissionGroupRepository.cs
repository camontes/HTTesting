namespace HR_Platform.Domain.PermissionGroups;

public interface IPermissionGroupRepository
{
    Task<List<PermissionGroup>> GetAll();
}
