using HR_Platform.Domain.DefaultProfessionalAdvices;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultProfessionalAdviceRepository(ApplicationDbContext context) : IDefaultProfessionalAdviceRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultProfessionalAdvice>> GetAll() => await _context.DefaultProfessionalAdvices.AsNoTracking().ToListAsync();
}