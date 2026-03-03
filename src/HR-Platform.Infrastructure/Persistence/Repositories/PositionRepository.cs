using HR_Platform.Domain.Assignations;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Positions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class PositionRepository(ApplicationDbContext context) : IPositionRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<Position>> GetByCompanyIdAsync(CompanyId companyId) =>
        await _context.Positions.Where(p => p.CompanyId == companyId)
        .Include(p => p.Collaborators)
        .ToListAsync();

    public async Task<Position?> GetCollaboratorPositionByCompanyIdAsync(CompanyId companyId) =>
        await _context.Positions.Include(x => x.Collaborators).Where(p => p.CompanyId == companyId && p.Name == "Colaborador").FirstOrDefaultAsync();

    public async Task<bool> ExistsAsync(PositionId id) => await _context.Positions.AsNoTracking().AnyAsync(j => j.Id == id);
    public void Add(Position position) => _context.Positions.Add(position);
    public void Delete(Position position) => _context.Positions.Remove(position);
    public void Update(Position position) => _context.Positions.Update(position);
    public void DeleteRange(List<Position> positions) => _context.Positions.RemoveRange(positions);
    public async Task<Position?> GetByIdAsync(PositionId id) => await _context.Positions
        .Include(p => p.EvaluationCriterias)
        .ThenInclude(c => c.EvaluationCriteriaScores)
        .AsNoTracking()
        .SingleOrDefaultAsync(p => p.Id == id);

    public async Task<Position?> GetByIdWithoutChildrenAsync(PositionId id) => await _context.Positions
        .AsNoTracking()
        .SingleOrDefaultAsync(p => p.Id == id);
    public async Task<List<Position>> GetDefaultsAsync() => await _context.Positions.AsNoTracking().Where(j => j.Name == "Colaborador").ToListAsync();
    public async Task<List<Position>> GetAll() => await _context.Positions.Include(x => x.Collaborators).AsNoTracking().ToListAsync();

}