using HR_Platform.Domain.BloodTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class BloodTypeRepository(ApplicationDbContext context) : IBloodTypeRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<BloodType>> GetAll() => await _context.BloodTypes.AsNoTracking().ToListAsync();
}