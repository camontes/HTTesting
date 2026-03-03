using HR_Platform.Domain.EventRecurrences;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class EventRecurrenceRepository(ApplicationDbContext context) : IEventRecurrenceRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(EventRecurrence EventRecurrence) => _context.EventRecurrence.Add(EventRecurrence);

        public void Delete(EventRecurrence EventRecurrence) => _context.EventRecurrence.Remove(EventRecurrence);

        public void DeleteRange(List<EventRecurrence> tags) => _context.EventRecurrence.RemoveRange(tags);

        public void Update(EventRecurrence EventRecurrence) => _context.EventRecurrence.Update(EventRecurrence);

        public async Task<List<EventRecurrence>> GetAll() =>
            await _context.EventRecurrence
            .AsNoTracking()
            .ToListAsync();

        public async Task<EventRecurrence?> GetByIdAsync(EventRecurrenceId Id) =>
            await _context.EventRecurrence
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public void AddRangeEventRecurrence(List<EventRecurrence> EventRecurrence) =>
            _context.EventRecurrence
            .AddRange(EventRecurrence);
        public void AddRangeEventRecurrences(List<EventRecurrence> EventRecurrences) => _context.EventRecurrence.AddRange(EventRecurrences);
    }
}
