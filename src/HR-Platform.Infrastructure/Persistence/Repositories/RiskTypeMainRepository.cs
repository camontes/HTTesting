using HR_Platform.Domain.Companies;
using HR_Platform.Domain.RiskTypeMains;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class RiskTypeMainRepository(ApplicationDbContext context) : IRiskTypeMainRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(RiskTypeMain RiskTypeMain) => _context.RiskTypeMains.Add(RiskTypeMain);
        public void Delete(RiskTypeMain RiskTypeMain) => _context.RiskTypeMains.Remove(RiskTypeMain);
        public void Update(RiskTypeMain RiskTypeMain) => _context.RiskTypeMains.Update(RiskTypeMain);
        public async Task<List<RiskTypeMain>> GetAll() => await _context.RiskTypeMains
            .AsNoTracking()
            .ToListAsync();

        public async Task<RiskTypeMain?> GetByIdAsync(RiskTypeMainId Id) =>
            await _context.RiskTypeMains
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<RiskTypeMain?> GetNoneRiskTypeMainByCompanyIdAsync(CompanyId companyId) =>
            await _context.RiskTypeMains
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();

        public async Task<List<RiskTypeMain>?> GetByCompanyIdAsync(CompanyId companyId) =>
                 await _context.RiskTypeMains.Where(p => p.CompanyId == companyId && p.Name != "Ninguno").ToListAsync();
    }
}
