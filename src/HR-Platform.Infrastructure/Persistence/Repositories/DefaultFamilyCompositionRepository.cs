using HR_Platform.Domain.DefaultFamilyCompositions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultFamilyCompositionRepository(ApplicationDbContext context) : IDefaultFamilyCompositionRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultFamilyComposition>> GetAll() => await _context.DefaultFamilyCompositions.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}