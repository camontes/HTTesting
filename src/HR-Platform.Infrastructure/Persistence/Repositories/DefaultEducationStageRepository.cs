using HR_Platform.Domain.DefaultEducationStages;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultEducationStageRepository(ApplicationDbContext context) : IDefaultEducationStageRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultEducationStage>> GetAll() => await _context.DefaultEducationStages.AsNoTracking().ToListAsync();
}