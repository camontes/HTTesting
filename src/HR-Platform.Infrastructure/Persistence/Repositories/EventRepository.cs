using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class EventRepository(ApplicationDbContext context) : IEventRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Event Event) => _context.Event.Add(Event);

        public void Delete(Event Event) => _context.Event.Remove(Event);

        public void DeleteRange(List<Event> tags) => _context.Event.RemoveRange(tags);

        public void Update(Event Event) => _context.Event.Update(Event);

        public async Task<List<Event>> GetAll() =>
            await _context.Event
            .AsNoTracking()
            .ToListAsync();

        public async Task<Event?> GetByIdAsync(EventId Id) =>
            await _context.Event
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public void AddRangeEvent(List<Event> Event) =>
            _context.Event
            .AddRange(Event);

        public async Task<List<Event>?> GetByCompanyIdAsync(CompanyId CompanyId) =>
        await _context.Event
            .AsNoTracking()
            .Where(r => r.CompanyId == CompanyId).ToListAsync();

        public void AddRangeEvents(List<Event> Events) => _context.Event.AddRange(Events);
    }
}
