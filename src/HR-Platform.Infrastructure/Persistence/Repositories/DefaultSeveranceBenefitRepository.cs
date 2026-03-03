using HR_Platform.Domain.DefaultSeveranceBenefits;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultSeveranceBenefitRepository(ApplicationDbContext context) : IDefaultSeveranceBenefitRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultSeveranceBenefit>> GetAll() => await _context.DefaultSeveranceBenefits.AsNoTracking().ToListAsync();
}