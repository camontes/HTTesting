using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SeveranceBenefits;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class SeveranceBenefitRepository(ApplicationDbContext context) : ISeveranceBenefitRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(SeveranceBenefit SeveranceBenefit) => _context.SeveranceBenefits.Add(SeveranceBenefit);

        public void Delete(SeveranceBenefit SeveranceBenefit) => _context.SeveranceBenefits.Remove(SeveranceBenefit);

        public void DeleteRange(List<SeveranceBenefit> severanceBenefits) => _context.SeveranceBenefits.RemoveRange(severanceBenefits);

        public void Update(SeveranceBenefit SeveranceBenefit) => _context.SeveranceBenefits.Update(SeveranceBenefit);

        public async Task<bool> ExistsAsync(SeveranceBenefitId id) => await _context.SeveranceBenefits.AsNoTracking().AnyAsync(r => r.Id == id);

        public async Task<List<SeveranceBenefit>> GetAll() => await _context.SeveranceBenefits.AsNoTracking().ToListAsync();

        public async Task<SeveranceBenefit?> GetByIdAsync(SeveranceBenefitId Id) =>
            await _context.SeveranceBenefits.AsNoTracking().SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<SeveranceBenefit?> GetNoneSeveranceBenefitByCompanyIdAsync(CompanyId companyId) =>
            await _context.SeveranceBenefits.Where(p => p.CompanyId == companyId && p.Name == "Ninguno").FirstOrDefaultAsync();
        public void AddRangeSeveranceBenefits(List<SeveranceBenefit> SeveranceBenefits) => _context.SeveranceBenefits.AddRange(SeveranceBenefits);

        public async Task<List<SeveranceBenefit>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize)
        {
            if (page == 0 && pageSize == 0)
            {
                return await _context.SeveranceBenefits.Include(c => c.Collaborators)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
            }
            else
            {
                return await _context.SeveranceBenefits.Include(c => c.Collaborators)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetNumberOfSeveranceBenefits(CompanyId id)
        {
            List<SeveranceBenefit>? amount = await _context.SeveranceBenefits.Where(p => p.CompanyId == id).ToListAsync();
            return amount.Count - 1;
        }
    }
}
