using HR_Platform.Domain.EconomicLevels;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class EconomicLevelRepository(ApplicationDbContext context) : IEconomicLevelRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<EconomicLevel>> GetAll() => await _context.EconomicLevels.AsNoTracking().ToListAsync();
}