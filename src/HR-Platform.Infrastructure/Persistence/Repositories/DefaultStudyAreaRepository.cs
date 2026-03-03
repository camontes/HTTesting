using HR_Platform.Domain.DefaultStudyAreas;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultStudyAreaRepository(ApplicationDbContext context) : IDefaultStudyAreaRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultStudyArea>> GetAll() => await _context.DefaultStudyAreas.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}