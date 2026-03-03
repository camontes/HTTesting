using HR_Platform.Domain.PriorityNovelties;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class PriorityNoveltyRepository(ApplicationDbContext context) : IPriorityNoveltyRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<PriorityNovelty>> GetAll() => await _context.PriorityNovelties.AsNoTracking().ToListAsync();
}