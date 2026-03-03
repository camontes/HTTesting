using HR_Platform.Domain.DefaultLanguageTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultLanguageTypeRepository(ApplicationDbContext context) : IDefaultLanguageTypeRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultLanguageType>> GetAll() => await _context.DefaultLanguageTypes.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}