using HR_Platform.Domain.DefaultLifePreferences;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultLifePreferenceRepository(ApplicationDbContext context) : IDefaultLifePreferenceRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultLifePreference>> GetAll() => await _context.DefaultLifePreferences.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}