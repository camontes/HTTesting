using HR_Platform.Domain.Assignations;
using HR_Platform.Domain.AssignationTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class AssignationTypeRepository(ApplicationDbContext context) : IAssignationTypeRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<AssignationType>> GetAll() => await _context.AssignationTypes.AsNoTracking().ToListAsync();

    public async Task<bool> ExistsAsync(AssignationTypeId id) => await _context.AssignationTypes.AsNoTracking().AnyAsync(j => j.Id == id);
}