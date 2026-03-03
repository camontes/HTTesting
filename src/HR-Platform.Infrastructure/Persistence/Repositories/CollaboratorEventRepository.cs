using HR_Platform.Domain.CollaboratorEvents;
using HR_Platform.Domain.Collaborators;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class CollaboratorEventRepository(ApplicationDbContext context) : ICollaboratorEventRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(CollaboratorEvent CollaboratorEvent) => _context.CollaboratorEvents.Add(CollaboratorEvent);
        public void AddRange(List<CollaboratorEvent> CollaboratorEvents) => _context.CollaboratorEvents.AddRange(CollaboratorEvents);

        public void Delete(CollaboratorEvent CollaboratorEvent) => _context.CollaboratorEvents.Remove(CollaboratorEvent);
        
        public void DeleteRange(List<CollaboratorEvent> tags) => _context.CollaboratorEvents.RemoveRange(tags);

        public void Update(CollaboratorEvent CollaboratorEvent) => _context.CollaboratorEvents.Update(CollaboratorEvent);

        public async Task<List<CollaboratorEvent>> GetAll() =>
            await _context.CollaboratorEvents
            .AsNoTracking()
            .ToListAsync();

        public async Task<CollaboratorEvent?> GetByIdAsync(CollaboratorEventId Id) =>
            await _context.CollaboratorEvents
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public void AddRangeCollaboratorEvents(List<CollaboratorEvent> CollaboratorEvents) => 
            _context.CollaboratorEvents
            .AddRange(CollaboratorEvents);

        public async Task<List<CollaboratorEvent>> GetByCollaboratorIdAsync(CollaboratorId id) =>
            await _context.CollaboratorEvents
            .Where(x => x.CollaboratorId == id)
            .Include(q => q.Collaborator)
            .Include(w => w.Event)
            .ThenInclude(e => e.TimeZone)
            .Include(w => w.Event)
            .ThenInclude(e => e.EventType)
            .Include(w => w.Event)
            .ThenInclude(e => e.CollaboratorEvents)
            .ThenInclude(r => r.Collaborator)
            .ToListAsync();


    }
}
