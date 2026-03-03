using HR_Platform.Domain.CollaboratorSoftSkills;
using HR_Platform.Domain.Collaborators;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class CollaboratorSoftSkillRepository(ApplicationDbContext context) : ICollaboratorSoftSkillRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(CollaboratorSoftSkill CollaboratorSoftSkill) => _context.Add(CollaboratorSoftSkill);

    public void AddRange(List<CollaboratorSoftSkill> CollaboratorSoftSkill) => _context.AddRange(CollaboratorSoftSkill);

    public void Delete(CollaboratorSoftSkill CollaboratorSoftSkill) => _context.Remove(CollaboratorSoftSkill); 

    public void DeleteRange(List<CollaboratorSoftSkill> CollaboratorSoftSkill)=> _context.RemoveRange(CollaboratorSoftSkill);

    public void Update(CollaboratorSoftSkill CollaboratorSoftSkill) => _context.Update(CollaboratorSoftSkill);

    public void UpdateRange(List<CollaboratorSoftSkill> CollaboratorSoftSkill) => _context.UpdateRange(CollaboratorSoftSkill);

    public async Task<List<CollaboratorSoftSkill>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
        await _context.CollaboratorSoftSkills.Where(x => x.CollaboratorId == collaboratorId).Include(x=>x.DefaultSoftSkill).ToListAsync();

    public async Task<CollaboratorSoftSkill?> GetByIdAsync(CollaboratorSoftSkillId id) =>
        await _context.CollaboratorSoftSkills.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);

    public async Task DeleteById(CollaboratorSoftSkillId CollaboratorSoftSkillId)
    {
        CollaboratorSoftSkill? collaboratorSoftSkill = await _context.CollaboratorSoftSkills.FindAsync(CollaboratorSoftSkillId);
        if (collaboratorSoftSkill != null)
        {
            _context.CollaboratorSoftSkills.Remove(collaboratorSoftSkill);
        }
    }
}
