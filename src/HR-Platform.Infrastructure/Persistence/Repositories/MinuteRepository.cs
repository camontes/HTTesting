using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Minutes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class MinuteRepository(ApplicationDbContext context) : IMinuteRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Minute Minute) => _context.Minutes.Add(Minute);
        public void AddRange(List<Minute> Minutes) => _context.Minutes.AddRange(Minutes);

        public void Delete(Minute Minute) => _context.Minutes.Remove(Minute);
        public void Update(Minute Minute) => _context.Minutes.Update(Minute);

        public async Task<bool> ExistsAsync(MinuteId id) => await _context.Minutes
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<Minute?> GetByIdAsync(MinuteId Id) =>
            await _context.Minutes
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<Minute?> GetNoneMinuteByCompanyIdAsync(CompanyId companyId) =>
            await _context.Minutes
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        
        public async Task<List<Minute>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.Minutes
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
    }
}
