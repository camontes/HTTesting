using HR_Platform.Domain.DefaultTimeZones;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultTimeZoneRepository(ApplicationDbContext context) : IDefaultTimeZoneRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultTimeZone>> GetAll() => await _context.DefaultTimeZones.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
    public async Task<bool> ExistsAsync(DefaultTimeZoneId id) => await _context.DefaultTimeZones.AsNoTracking().AnyAsync(j => j.Id == id);
}