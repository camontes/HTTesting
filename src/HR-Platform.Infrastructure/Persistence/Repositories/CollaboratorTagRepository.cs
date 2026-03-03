using HR_Platform.Domain.CollaboratorTags;
using HR_Platform.Domain.Collaborators;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.Tags;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class CollaboratorTagRepository(ApplicationDbContext context) : ICollaboratorTagRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(CollaboratorTag CollaboratorTag) => _context.Add(CollaboratorTag);

    public void AddRange(List<CollaboratorTag> CollaboratorTag) => _context.AddRange(CollaboratorTag);

    public void Delete(CollaboratorTag CollaboratorTag) => _context.Remove(CollaboratorTag); 

    public void DeleteRange(List<CollaboratorTag> CollaboratorTag)=> _context.RemoveRange(CollaboratorTag);

    public void Update(CollaboratorTag CollaboratorTag) => _context.Update(CollaboratorTag);

    public void UpdateRange(List<CollaboratorTag> CollaboratorTag) => _context.UpdateRange(CollaboratorTag);

    public async Task<List<CollaboratorTag>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
        await _context.CollaboratorTag.Include(x => x.Tag).Where(x => x.CollaboratorId == collaboratorId).ToListAsync();

    public async Task<CollaboratorTag?> GetByIdAsync(CollaboratorTagId id) =>
        await _context.CollaboratorTag.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);

    public async Task DeleteById(CollaboratorTagId collaboratorLanguageId)
    {
        CollaboratorTag? collaboratorLanguage = await _context.CollaboratorTag.FindAsync(collaboratorLanguageId);
        if (collaboratorLanguage != null)
        {
            _context.CollaboratorTag.Remove(collaboratorLanguage);
        }
    }

    public async Task<bool> IsExistCollaboratorAsync(TagId tagId, CollaboratorId collaboratorId) =>
        await _context.CollaboratorTag
        .AsNoTracking()
        .AnyAsync(x => x.TagId == tagId && x.CollaboratorId == collaboratorId);

    public async Task<bool> IsExistTagNameAsync(string tagName, CollaboratorId collaboratorId) =>
        await _context.CollaboratorTag
        .AsNoTracking()
        .Include(t => t.Tag)
        .AnyAsync(x => x.Tag.Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase) && x.CollaboratorId == collaboratorId);
}
