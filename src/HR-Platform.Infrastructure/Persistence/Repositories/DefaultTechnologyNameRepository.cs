using HR_Platform.Domain.DefaultTechnologyNames;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultTechnologyNameRepository(ApplicationDbContext context) : IDefaultTechnologyNameRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultTechnologyName>> GetAll() => await _context.DefaultTechnologyNames.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}