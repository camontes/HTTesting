using HR_Platform.Domain.DefaultMonths;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultMonthRepository(ApplicationDbContext context) : IDefaultMonthRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultMonth>> GetAll() => await _context.DefaultMonths.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}