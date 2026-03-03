using HR_Platform.Domain.CollaboratorEducations;
using HR_Platform.Domain.Collaborators;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class CollaboratorEducationRepository(ApplicationDbContext context) : ICollaboratorEducationRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(CollaboratorEducation CollaboratorEducation) => _context.Add(CollaboratorEducation);

    public void AddRange(List<CollaboratorEducation> CollaboratorEducation) => _context.AddRange(CollaboratorEducation);

    public void Delete(CollaboratorEducation CollaboratorEducation) => _context.Remove(CollaboratorEducation); 

    public void DeleteRange(List<CollaboratorEducation> CollaboratorEducation)=> _context.RemoveRange(CollaboratorEducation);

    public void Update(CollaboratorEducation CollaboratorEducation) => _context.Update(CollaboratorEducation);

    public void UpdateRange(List<CollaboratorEducation> CollaboratorEducation) => _context.UpdateRange(CollaboratorEducation);

    public async Task<List<CollaboratorEducation>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
        await _context.CollaboratorEducations.Where(x => x.CollaboratorId == collaboratorId)
        .Include(p=> p.DefaultProfession)
        .Include(sa => sa.DefaultStudyArea)
        .Include(st => st.DefaultStudyType)
        .Include(es => es.DefaultEducationStage)
        .ToListAsync();

    public async Task<CollaboratorEducation?> GetByIdAsync(CollaboratorEducationId id) =>
        await _context.CollaboratorEducations.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);

    public async Task DeleteById(CollaboratorEducationId collaboratorEducationId)
    {
        CollaboratorEducation? collaboratorEducation = await _context.CollaboratorEducations.FindAsync(collaboratorEducationId);
        if (collaboratorEducation != null)
        {
            _context.CollaboratorEducations.Remove(collaboratorEducation);
        }
    }
}
