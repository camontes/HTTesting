using HR_Platform.Domain.CollaboratorLifePreferences;
using HR_Platform.Domain.Collaborators;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class CollaboratorLifePreferenceRepository(ApplicationDbContext context) : ICollaboratorLifePreferenceRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(CollaboratorLifePreference CollaboratorLifePreference) => _context.Add(CollaboratorLifePreference);

    public void AddRange(List<CollaboratorLifePreference> CollaboratorLifePreference) => _context.AddRange(CollaboratorLifePreference);

    public void Delete(CollaboratorLifePreference CollaboratorLifePreference) => _context.Remove(CollaboratorLifePreference); 

    public void DeleteRange(List<CollaboratorLifePreference> CollaboratorLifePreference)=> _context.RemoveRange(CollaboratorLifePreference);

    public void Update(CollaboratorLifePreference CollaboratorLifePreference) => _context.Update(CollaboratorLifePreference);

    public void UpdateRange(List<CollaboratorLifePreference> CollaboratorLifePreference) => _context.UpdateRange(CollaboratorLifePreference);

    public async Task<List<CollaboratorLifePreference>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
        await _context.CollaboratorLifePreferences.Where(x => x.CollaboratorId == collaboratorId) 
        .Include(z => z.DefaultLifePreference).ToListAsync();

    public async Task<CollaboratorLifePreference?> GetByIdAsync(CollaboratorLifePreferenceId id) =>
        await _context.CollaboratorLifePreferences.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);

    public async Task DeleteById(CollaboratorLifePreferenceId collaboratorLifePreferenceId)
    {
        CollaboratorLifePreference? collaboratorLifePreference = await _context.CollaboratorLifePreferences.FindAsync(collaboratorLifePreferenceId);
        if (collaboratorLifePreference != null)
        {
            _context.CollaboratorLifePreferences.Remove(collaboratorLifePreference);
        }
    }
}
