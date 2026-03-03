using HR_Platform.Domain.DefaultEventTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultEventTypeRepository(ApplicationDbContext context) : IDefaultEventTypeRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultEventType>> GetAll() => await _context.DefaultEventTypes.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
    public async Task<bool> ExistsAsync(DefaultEventTypeId id) => await _context.DefaultEventTypes.AsNoTracking().AnyAsync(j => j.Id == id);
}