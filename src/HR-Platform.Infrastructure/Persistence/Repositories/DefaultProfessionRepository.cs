using HR_Platform.Domain.DefaultProfessions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultProfessionRepository(ApplicationDbContext context) : IDefaultProfessionRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultProfession>> GetAll() => await _context.DefaultProfessions.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();

    public async Task<DefaultProfession?> GetOtheProfessionId() => await _context.DefaultProfessions.Where(h => h.Name != "Ninguno").AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Otra");
}