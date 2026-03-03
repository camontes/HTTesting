using HR_Platform.Domain.DefaultStudyTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultStudyTypeRepository(ApplicationDbContext context) : IDefaultStudyTypeRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultStudyType>> GetAll() => await _context.DefaultStudyTypes.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}