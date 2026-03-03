using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Benefits;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class BenefitRepository(ApplicationDbContext context) : IBenefitRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Benefit Benefit) => _context.Benefits.Add(Benefit);
        public void AddRange(List<Benefit> Benefits) => _context.Benefits.AddRange(Benefits);

        public void Delete(Benefit Benefit) => _context.Benefits.Remove(Benefit);
        public void Update(Benefit Benefit) => _context.Benefits.Update(Benefit);

        public async Task<bool> ExistsAsync(BenefitId id) => await _context.Benefits
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<Benefit?> GetByIdAsync(BenefitId Id) =>
            await _context.Benefits
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<Benefit?> GetNoneBenefitByCompanyIdAsync(CompanyId companyId) =>
            await _context.Benefits
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        
        public async Task<List<Benefit>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.Benefits
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
    }
}
