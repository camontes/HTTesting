using HR_Platform.Domain.DefaultDaysOfWeeks;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultDaysOfWeekRepository(ApplicationDbContext context) : IDefaultDaysOfWeekRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultDaysOfWeek>> GetAll() => await _context.DefaultDaysOfWeeks.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}