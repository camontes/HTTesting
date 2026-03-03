using HR_Platform.Domain.DefaultTags;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultTagRepository(ApplicationDbContext context) : IDefaultTagRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultTag>> GetAll() => await _context.DefaultTags.AsNoTracking().ToListAsync();
}