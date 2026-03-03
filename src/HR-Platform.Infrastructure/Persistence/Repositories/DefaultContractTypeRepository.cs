using HR_Platform.Domain.DefaultContractTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultContractTypeRepository(ApplicationDbContext context) : IDefaultContractTypeRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultContractType>> GetAll() => await _context.DefaultContractTypes.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
}