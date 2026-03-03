using HR_Platform.Domain.DefaultEventReplays;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultEventReplayRepository(ApplicationDbContext context) : IDefaultEventReplayRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultEventReplay>> GetAll() => await _context.DefaultEventReplays.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
    public async Task<bool> ExistsAsync(DefaultEventReplayId id) => await _context.DefaultEventReplays.AsNoTracking().AnyAsync(j => j.Id == id);
}