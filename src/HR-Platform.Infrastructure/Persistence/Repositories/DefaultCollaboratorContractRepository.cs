using HR_Platform.Domain.DefaultCollaboratorContracts;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultCollaboratorContractRepository(ApplicationDbContext context) : IDefaultCollaboratorContractRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultCollaboratorContract>> GetAll() => await _context.DefaultCollaboratorContracts.AsNoTracking().ToListAsync();
}