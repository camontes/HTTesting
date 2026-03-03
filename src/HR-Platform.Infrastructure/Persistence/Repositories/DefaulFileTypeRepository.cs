using HR_Platform.Domain.DefaultFileTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultFileTypeRepository(ApplicationDbContext context) : IDefaultFileTypeRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultFileType>> GetAll() => await _context.DefaultFileTypes.AsNoTracking().ToListAsync();
}