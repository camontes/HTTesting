using HR_Platform.Domain.DefaultSoftSkills;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultSoftSkillRepository(ApplicationDbContext context) : IDefaultSoftSkillRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultSoftSkill>> GetAll() => await _context.DefaultSoftSkills.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}