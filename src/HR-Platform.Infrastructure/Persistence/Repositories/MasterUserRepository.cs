using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.MasterUsers;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class MasterUserRepository(ApplicationDbContext context) : IMasterUserRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Update(MasterUser masterUser) => _context.MasterUsers.Update(masterUser);
    public async Task<bool> ExistsAsync(MasterUserId id) => await _context.MasterUsers.AsNoTracking().AnyAsync(m => m.Id == id);
    public async Task<MasterUser?> GetByIdAsync(MasterUserId id) => await _context.MasterUsers.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
    public async Task<MasterUser?> GetByEmailAsync(string email) => await _context.MasterUsers.AsNoTracking().SingleOrDefaultAsync(c => c.Email == Email.Create(email));
}