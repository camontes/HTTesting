
namespace HR_Platform.Domain.MasterUsers;

public interface IMasterUserRepository
{
    Task<bool> ExistsAsync(MasterUserId id);
    Task<MasterUser?> GetByIdAsync(MasterUserId id);
    Task<MasterUser?> GetByEmailAsync(string email);
    void Update(MasterUser masterUser);
}
