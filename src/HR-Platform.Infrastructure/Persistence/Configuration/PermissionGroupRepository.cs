using HR_Platform.Domain.PermissionGroups;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class PermissionGroupRepository : IPermissionGroupRepository
{
    private readonly ApplicationDbContext _context;

    public PermissionGroupRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<PermissionGroup>> GetAll() => await _context.PermissionGroups
        .AsNoTracking()
        .Include(pg => pg.Permissions)
        .ThenInclude(p => p.RolesPermissions)
        .ToListAsync();
}