using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.CollaboratorTalentPools;
using HR_Platform.Domain.TalentPools;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class CollaboratorTalentPoolRepository(ApplicationDbContext context) : ICollaboratorTalentPoolRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(CollaboratorTalentPool CollaboratorTalentPool) => _context.Add(CollaboratorTalentPool);

    public void AddRange(List<CollaboratorTalentPool> CollaboratorTalentPool) => _context.AddRange(CollaboratorTalentPool);

    public void Delete(CollaboratorTalentPool CollaboratorTalentPool) => _context.Remove(CollaboratorTalentPool);

    public void DeleteRange(List<CollaboratorTalentPool> CollaboratorTalentPool) => _context.RemoveRange(CollaboratorTalentPool);

    public void Update(CollaboratorTalentPool CollaboratorTalentPool) => _context.Update(CollaboratorTalentPool);

    public void UpdateRange(List<CollaboratorTalentPool> CollaboratorTalentPool) => _context.UpdateRange(CollaboratorTalentPool);

    public async Task<List<CollaboratorTalentPool>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
        await _context.CollaboratorTalentPool.Where(x => x.CollaboratorId == collaboratorId)
        .Include(z => z.TalentPool).ToListAsync();

    public async Task<List<CollaboratorTalentPool>> GetByTalentPoolIdAsync(TalentPoolId talentPoolId) =>
        await _context.CollaboratorTalentPool
        .Where(x => x.TalentPoolId == talentPoolId)
        .Include(z => z.Collaborator)
        .ThenInclude(y => y.Assignation)
        .Include(r => r.Collaborator)
        .ThenInclude(d => d.DocumentType)
        .ToListAsync();

    public async Task<CollaboratorTalentPool?> GetByIdAsync(CollaboratorTalentPoolId id) =>
        await _context.CollaboratorTalentPool.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);

    public async Task DeleteById(CollaboratorTalentPoolId collaboratorLifePreferenceId)
    {
        CollaboratorTalentPool? collaboratorLifePreference = await _context.CollaboratorTalentPool.FindAsync(collaboratorLifePreferenceId);
        if (collaboratorLifePreference != null)
        {
            _context.CollaboratorTalentPool.Remove(collaboratorLifePreference);
        }
    }

    public async Task<bool> IsExistCollaboratorAsync(TalentPoolId talentPoolId, CollaboratorId collaboratorId) =>
        await _context.CollaboratorTalentPool
        .AsNoTracking()
        .AnyAsync(x => x.TalentPoolId == talentPoolId && x.CollaboratorId == collaboratorId);
}

