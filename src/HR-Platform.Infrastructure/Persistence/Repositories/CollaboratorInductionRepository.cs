using HR_Platform.Domain.CollaboratorInductions;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Inductions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class CollaboratorInductionRepository(ApplicationDbContext context) : ICollaboratorInductionRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(CollaboratorInduction CollaboratorInduction) => _context.Add(CollaboratorInduction);

    public void AddRange(List<CollaboratorInduction> CollaboratorInduction) => _context.AddRange(CollaboratorInduction);

    public void Delete(CollaboratorInduction CollaboratorInduction) => _context.Remove(CollaboratorInduction);

    public void DeleteRange(List<CollaboratorInduction> CollaboratorInduction) => _context.RemoveRange(CollaboratorInduction);

    public void Update(CollaboratorInduction CollaboratorInduction) => _context.Update(CollaboratorInduction);

    public void UpdateRange(List<CollaboratorInduction> CollaboratorInduction) => _context.UpdateRange(CollaboratorInduction);

    public async Task<List<CollaboratorInduction>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
        await _context.CollaboratorInductions.Where(x => x.CollaboratorId == collaboratorId)
        .Include(z => z.Induction).ToListAsync();

    public async Task<List<CollaboratorInduction>> GetByCollaboratorAndInductionIdAsync(CollaboratorId collaboratorId, InductionId inductionId) =>
        await _context.CollaboratorInductions.Where(x => x.CollaboratorId == collaboratorId && x.InductionId == inductionId)
        .Include(z => z.Induction).ToListAsync();
    public async Task<CollaboratorInduction?> GetByIdAsync(CollaboratorInductionId id) =>
        await _context.CollaboratorInductions.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);

    public async Task DeleteById(CollaboratorInductionId collaboratorLifePreferenceId)
    {
        CollaboratorInduction? collaboratorLifePreference = await _context.CollaboratorInductions.FindAsync(collaboratorLifePreferenceId);
        if (collaboratorLifePreference != null)
        {
            _context.CollaboratorInductions.Remove(collaboratorLifePreference);
        }
    }

    public async Task<List<CollaboratorInduction>> GetByInductionIdAsync(InductionId inductionId) =>
        await _context.CollaboratorInductions.AsNoTracking().Include(x => x.Collaborator).Where(r => r.InductionId == inductionId).ToListAsync();


    public async Task<List<CollaboratorInduction>> GetAllAsync() =>
        await _context.CollaboratorInductions.AsNoTracking().ToListAsync();
}
