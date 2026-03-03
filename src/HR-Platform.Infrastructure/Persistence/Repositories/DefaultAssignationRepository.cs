using HR_Platform.Domain.DefaultAssignations;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class DefaultAssignationRepository(ApplicationDbContext context) : IDefaultAssignationRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<DefaultAssignation>> GetAll() => await _context.DefaultAssignations.AsNoTracking().ToListAsync();
}