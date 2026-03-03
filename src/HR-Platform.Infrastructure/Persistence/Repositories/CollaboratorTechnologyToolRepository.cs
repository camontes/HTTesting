using HR_Platform.Domain.CollaboratorTechnologyTools;
using HR_Platform.Domain.Collaborators;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories;

public class CollaboratorTechnologyToolRepository(ApplicationDbContext context) : ICollaboratorTechnologyToolRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public void Add(CollaboratorTechnologyTool CollaboratorTechnologyTool) => _context.Add(CollaboratorTechnologyTool);

    public void AddRange(List<CollaboratorTechnologyTool> CollaboratorTechnologyTool) => _context.AddRange(CollaboratorTechnologyTool);

    public void Delete(CollaboratorTechnologyTool CollaboratorTechnologyTool) => _context.Remove(CollaboratorTechnologyTool); 

    public void DeleteRange(List<CollaboratorTechnologyTool> CollaboratorTechnologyTool)=> _context.RemoveRange(CollaboratorTechnologyTool);

    public void Update(CollaboratorTechnologyTool CollaboratorTechnologyTool) => _context.Update(CollaboratorTechnologyTool);

    public void UpdateRange(List<CollaboratorTechnologyTool> CollaboratorTechnologyTool) => _context.UpdateRange(CollaboratorTechnologyTool);

    public async Task<List<CollaboratorTechnologyTool>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
        await _context.CollaboratorTechnologyTools.Where(x => x.CollaboratorId == collaboratorId).Include(x =>x.DefaultKnowledgeLevel)
        .Include(z=> z.DefaultTechnologyName).ToListAsync();

    public async Task<CollaboratorTechnologyTool?> GetByIdAsync(CollaboratorTechnologyToolId id) =>
        await _context.CollaboratorTechnologyTools.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);

    public async Task DeleteById(CollaboratorTechnologyToolId collaboratorTechnologyToolId)
    {
        CollaboratorTechnologyTool? collaboratorTechnologyTool = await _context.CollaboratorTechnologyTools.FindAsync(collaboratorTechnologyToolId);
        if (collaboratorTechnologyTool != null)
        {
            _context.CollaboratorTechnologyTools.Remove(collaboratorTechnologyTool);
        }
    }
}
