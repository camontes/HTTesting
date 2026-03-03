using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EventTypes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class EventTypeRepository(ApplicationDbContext context) : IEventTypeRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(EventType EventType) => _context.EventTypes.Add(EventType);

        public void Delete(EventType EventType) => _context.EventTypes.Remove(EventType);

        public void DeleteRange(List<EventType> tags) => _context.EventTypes.RemoveRange(tags);

        public void Update(EventType EventType) => _context.EventTypes.Update(EventType);

        public async Task<List<EventType>> GetAll() =>
            await _context.EventTypes
            .Where(x => x.Name != "Ninguno")
            .AsNoTracking()
            .ToListAsync();

        public async Task<EventType?> GetByIdAsync(EventTypeId Id) =>
            await _context.EventTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public void AddRangeEventType(List<EventType> EventType) =>
            _context.EventTypes
            .AddRange(EventType);

        public async Task<List<EventType>?> GetByCompanyIdAsync(CompanyId CompanyId) =>
        await _context.EventTypes
            .AsNoTracking()
            .Where(r => r.CompanyId == CompanyId).ToListAsync();

        public void AddRangeEventTypes(List<EventType> EventTypes) => _context.EventTypes.AddRange(EventTypes);

        public async Task<EventType?> GetNoneEventTypeByCompanyIdAsync(CompanyId companyId) =>
         await _context.EventTypes
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
    }
}
