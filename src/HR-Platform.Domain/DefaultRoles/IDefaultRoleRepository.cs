namespace HR_Platform.Domain.DefaultRoles;

public interface IDefaultRoleRepository
{
    Task<List<DefaultRole>> GetAll();
}
