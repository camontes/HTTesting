using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Pensions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class PensionRepository(ApplicationDbContext context) : IPensionRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Pension Pension) => _context.Pensions.Add(Pension);

        public void Delete(Pension Pension) => _context.Pensions.Remove(Pension);
        public void DeleteRange(List<Pension> Pensions) => _context.Pensions.RemoveRange(Pensions);
        public void Update(Pension Pension) => _context.Pensions.Update(Pension);

        public async Task<bool> ExistsAsync(PensionId id) => await _context.Pensions
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<Pension>> GetAll() => await _context.Pensions
            .AsNoTracking()
            .ToListAsync();

        public async Task<Pension?> GetByIdAsync(PensionId Id) =>
            await _context.Pensions
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<Pension?> GetNonePensionByCompanyIdAsync(CompanyId companyId) =>
            await _context.Pensions
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        public void AddRangePensions(List<Pension> Pensions) => _context.Pensions
            .AddRange(Pensions);

        public async Task<List<Pension>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize)
        {
            if (page == 0 && pageSize == 0)
            {
                return await _context.Pensions.Include(c => c.Collaborators)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
            }
            else
            {
                return await _context.Pensions.Include(c => c.Collaborators)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetNumberOfPensions(CompanyId id) {
           List<Pension>? amount =  await _context.Pensions.Where(p => p.CompanyId == id).ToListAsync();
           return amount.Count - 1;
        }
    }
}
