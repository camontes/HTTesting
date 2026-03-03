using HR_Platform.Domain.MaritalStatuses;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class MaritalStatusRepository : IMaritalStatusRepository
{
    private readonly ApplicationDbContext _context;

    public MaritalStatusRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<MaritalStatus>> GetAll() => await _context.MaritalStatuses.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}