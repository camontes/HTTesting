using HR_Platform.Domain.Companies;
using HR_Platform.Domain.CoexistenceCommitteeMinutes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class CoexistenceCommitteeMinuteRepository(ApplicationDbContext context) : ICoexistenceCommitteeMinuteRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(CoexistenceCommitteeMinute CoexistenceCommitteeMinute) => _context.CoexistenceCommitteeMinutes.Add(CoexistenceCommitteeMinute);
        public void AddRange(List<CoexistenceCommitteeMinute> CoexistenceCommitteeMinutes) => _context.CoexistenceCommitteeMinutes.AddRange(CoexistenceCommitteeMinutes);

        public void Delete(CoexistenceCommitteeMinute CoexistenceCommitteeMinute) => _context.CoexistenceCommitteeMinutes.Remove(CoexistenceCommitteeMinute);
        public void Update(CoexistenceCommitteeMinute CoexistenceCommitteeMinute) => _context.CoexistenceCommitteeMinutes.Update(CoexistenceCommitteeMinute);

        public async Task<bool> ExistsAsync(CoexistenceCommitteeMinuteId id) => await _context.CoexistenceCommitteeMinutes
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<CoexistenceCommitteeMinute?> GetByIdAsync(CoexistenceCommitteeMinuteId Id) =>
            await _context.CoexistenceCommitteeMinutes
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<CoexistenceCommitteeMinute?> GetNoneCoexistenceCommitteeMinuteByCompanyIdAsync(CompanyId companyId) =>
            await _context.CoexistenceCommitteeMinutes
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        
        public async Task<List<CoexistenceCommitteeMinute>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.CoexistenceCommitteeMinutes
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
    }
}
