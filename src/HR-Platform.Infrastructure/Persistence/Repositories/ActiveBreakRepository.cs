using HR_Platform.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.ActiveBreaks;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class ActiveBreakRepository(ApplicationDbContext context) : IActiveBreakRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<ActiveBreak?> GetByIdAsync(ActiveBreakId id) =>
            await _context.ActiveBreaks
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == id);

        public async Task<List<ActiveBreak>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.ActiveBreaks.ToListAsync();

        public async Task<bool> ExistsAsync(ActiveBreakId id) => await _context.ActiveBreaks
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public void Add(ActiveBreak activeBreak) => _context.ActiveBreaks.Add(activeBreak);
        public void AddRange(List<ActiveBreak> activeBreaks) => _context.ActiveBreaks.AddRange(activeBreaks);

        public void Delete(ActiveBreak activeBreak) => _context.ActiveBreaks.Remove(activeBreak);
        public void Update(ActiveBreak activeBreak) => _context.ActiveBreaks.Update(activeBreak);
    }
}
