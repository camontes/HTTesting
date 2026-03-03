using HR_Platform.Domain.DefaultPositions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultPositionRepository(ApplicationDbContext context) : IDefaultPositionRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultPosition>> GetAll() => await _context.DefaultPositions.AsNoTracking().ToListAsync();
}