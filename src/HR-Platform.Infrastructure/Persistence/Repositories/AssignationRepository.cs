using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Assignations;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class AssignationRepository(ApplicationDbContext context) : IAssignationRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(Assignation assignation) => _context.Assignations.Add(assignation);
    public void Delete(Assignation assignation) => _context.Assignations.Remove(assignation);
    public void Update(Assignation assignation) => _context.Assignations.Update(assignation);
    public void DeleteRange(List<Assignation> assignations) => _context.Assignations.RemoveRange(assignations);
    public async Task<bool> ExistsAsync(AssignationId id) => await _context.Assignations.AsNoTracking().AnyAsync(j => j.Id == id);
    public async Task<Assignation?> GetByIdAsync(AssignationId id) => await _context.Assignations.AsNoTracking().SingleOrDefaultAsync(j => j.Id == id);
    public async Task<List<Assignation>> GetDefaultsAsync() => await _context.Assignations.AsNoTracking().Where(j => j.Name == "Personal interno" || j.Name == "Personal externo").ToListAsync();
    public async Task<List<Assignation>?> GetByCompanyIdAsync(CompanyId companyId) => await _context.Assignations.AsNoTracking().Where(j => j.CompanyId == companyId).ToListAsync();
    public async Task<List<Assignation>?> GetByCompanyIdAndInternalOrExternalAsync(CompanyId companyId, bool IsInternalAssignation) 
        => await _context.Assignations.Include(z => z.Collaborators).AsNoTracking().Where(j => j.CompanyId == companyId && j.IsInternalAssignation == IsInternalAssignation).ToListAsync();
    public async Task<List<Assignation>> GetAll() => await _context.Assignations.Include(x => x.Collaborators).AsNoTracking().ToListAsync();

}