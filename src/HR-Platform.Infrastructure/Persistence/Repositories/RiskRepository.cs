using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Risks;
using HR_Platform.Domain.RiskTypeMains;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class RiskRepository(ApplicationDbContext context) : IRiskRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Risk Risk) => _context.Risks.Add(Risk);

        public void Delete(Risk Risk) => _context.Risks.Remove(Risk);
        public void Update(Risk Risk) => _context.Risks.Update(Risk);

        public async Task<bool> ExistsAsync(RiskId id) => await _context.Risks
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<Risk?> GetByIdAsync(RiskId Id) =>
            await _context.Risks
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<Risk>> GetRiskByRiskTypeIdAsync(RiskTypeMainId riskTypeId) =>
            await _context.Risks.Where(x => x.RiskTypeMainId == riskTypeId).ToListAsync();


    }
}
