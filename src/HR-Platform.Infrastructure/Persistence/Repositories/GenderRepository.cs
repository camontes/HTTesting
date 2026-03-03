using HR_Platform.Domain.Genders;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class GenderRepository(ApplicationDbContext context) : IGenderRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<Gender>> GetAll() => await _context.Genders.AsNoTracking().ToListAsync();
}