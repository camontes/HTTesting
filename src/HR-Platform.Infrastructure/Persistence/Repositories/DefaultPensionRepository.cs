using HR_Platform.Domain.DefaultPensions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultPensionRepository(ApplicationDbContext context) : IDefaultPensionRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultPension>> GetAll() => await _context.DefaultPensions.AsNoTracking().ToListAsync();
}