using HR_Platform.Domain.DefaultRepeatEveryEvents;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultRepeatEveryEventRepository(ApplicationDbContext context) : IDefaultRepeatEveryEventRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultRepeatEveryEvent>> GetAll() => await _context.DefaultRepeatEveryEvents.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}