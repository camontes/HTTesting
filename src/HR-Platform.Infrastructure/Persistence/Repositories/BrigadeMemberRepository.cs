using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.BrigadeMembers;
using HR_Platform.Domain.Collaborators;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class BrigadeMemberRepository(ApplicationDbContext context) : IBrigadeMemberRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(BrigadeMember BrigadeMember) => _context.Add(BrigadeMember);

    public void AddRange(List<BrigadeMember> BrigadeMember) => _context.AddRange(BrigadeMember);

    public void Delete(BrigadeMember BrigadeMember) => _context.Remove(BrigadeMember);

    public void DeleteRange(List<BrigadeMember> BrigadeMember) => _context.RemoveRange(BrigadeMember);

    public void Update(BrigadeMember BrigadeMember) => _context.Update(BrigadeMember);

    public void UpdateRange(List<BrigadeMember> BrigadeMember) => _context.UpdateRange(BrigadeMember);

    public async Task<List<BrigadeMember>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
        await _context.BrigadeMembers
        .Where(x => x.CollaboratorId == collaboratorId)
        .ToListAsync();

    public async Task<BrigadeMember?> GetByIdAsync(BrigadeMemberId id) =>
        await _context.BrigadeMembers.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);

    public async Task DeleteById(BrigadeMemberId collaboratorLifePreferenceId)
    {
        BrigadeMember? collaboratorLifePreference = await _context.BrigadeMembers.FindAsync(collaboratorLifePreferenceId);
        if (collaboratorLifePreference != null)
        {
            _context.BrigadeMembers.Remove(collaboratorLifePreference);
        }
    }

    public async Task<List<BrigadeMember>> GetAll() =>
        await _context.BrigadeMembers
        .Include(x => x.Collaborator)
        .ThenInclude(c => c.Position)
        .Include(z => z.BrigadeAdjustment).ToListAsync();

    public async Task<List<BrigadeMember>> GetByBrigadeAdjustmentIdAsync(BrigadeAdjustmentId brigadeAdjustmentId) =>
        await _context.BrigadeMembers.Where(x => x.BrigadeAdjustmentId == brigadeAdjustmentId).ToListAsync(); 
    
}
