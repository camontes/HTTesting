using HR_Platform.Domain.AssignationTypes;
using HR_Platform.Domain.DefaultCurrencyTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultCurrencyTypeRepository(ApplicationDbContext context) : IDefaultCurrencyTypeRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultCurrencyType>> GetAll() => await _context.DefaultCurrencyTypes.Where(h => h.Name != "Ninguno").AsNoTracking().ToListAsync();
    public async Task<bool> ExistsAsync(DefaultCurrencyTypeId id) => await _context.DefaultCurrencyTypes.AsNoTracking().AnyAsync(j => j.Id == id);
}