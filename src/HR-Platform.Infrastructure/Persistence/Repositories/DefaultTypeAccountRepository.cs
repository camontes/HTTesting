using HR_Platform.Domain.DefaultTypeAccounts;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultTypeAccountRepository(ApplicationDbContext context) : IDefaultTypeAccountRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultTypeAccount>> GetAll() => await _context.DefaultTypeAccounts.AsNoTracking().ToListAsync();
}