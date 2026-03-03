using HR_Platform.Domain.CollaboratorLanguages;
using HR_Platform.Domain.Collaborators;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class CollaboratorLanguageRepository(ApplicationDbContext context) : ICollaboratorLanguageRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(CollaboratorLanguage CollaboratorLanguage) => _context.Add(CollaboratorLanguage);

    public void AddRange(List<CollaboratorLanguage> CollaboratorLanguage) => _context.AddRange(CollaboratorLanguage);

    public void Delete(CollaboratorLanguage CollaboratorLanguage) => _context.Remove(CollaboratorLanguage); 

    public void DeleteRange(List<CollaboratorLanguage> CollaboratorLanguage)=> _context.RemoveRange(CollaboratorLanguage);

    public void Update(CollaboratorLanguage CollaboratorLanguage) => _context.Update(CollaboratorLanguage);

    public void UpdateRange(List<CollaboratorLanguage> CollaboratorLanguage) => _context.UpdateRange(CollaboratorLanguage);

    public async Task<List<CollaboratorLanguage>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
        await _context.CollaboratorLanguages.Where(x => x.CollaboratorId == collaboratorId).Include(x=> x.DefaultLanguageLevel)
        .Include(z => z.DefaultLanguageType).ToListAsync();

    public async Task<CollaboratorLanguage?> GetByIdAsync(CollaboratorLanguageId id) =>
        await _context.CollaboratorLanguages.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);

    public async Task DeleteById(CollaboratorLanguageId collaboratorLanguageId)
    {
        CollaboratorLanguage? collaboratorLanguage = await _context.CollaboratorLanguages.FindAsync(collaboratorLanguageId);
        if (collaboratorLanguage != null)
        {
            _context.CollaboratorLanguages.Remove(collaboratorLanguage);
        }
    }
}
