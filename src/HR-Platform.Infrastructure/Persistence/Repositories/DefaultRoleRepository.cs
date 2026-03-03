using HR_Platform.Domain.DefaultRoles;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultRoleRepository(ApplicationDbContext context) : IDefaultRoleRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultRole>> GetAll() => await _context.DefaultRoles.AsNoTracking().ToListAsync();
}