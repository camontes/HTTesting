using HR_Platform.Domain.DefaultKnowledgeLevels;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultKnowledgeLevelRepository(ApplicationDbContext context) : IDefaultKnowledgeLevelRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultKnowledgeLevel>> GetAll() => await _context.DefaultKnowledgeLevels.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}