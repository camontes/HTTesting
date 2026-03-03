using HR_Platform.Domain.DefaultLanguageLevels;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultLanguageLevelRepository(ApplicationDbContext context) : IDefaultLanguageLevelRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultLanguageLevel>> GetAll() => await _context.DefaultLanguageLevels.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}