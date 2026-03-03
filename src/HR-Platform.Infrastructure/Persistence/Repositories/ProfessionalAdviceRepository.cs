using HR_Platform.Domain.Companies;
using HR_Platform.Domain.ProfessionalAdvices;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class ProfessionalAdviceRepository(ApplicationDbContext context) : IProfessionalAdviceRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(ProfessionalAdvice ProfessionalAdvice) => _context.ProfessionalAdvices.Add(ProfessionalAdvice);

        public void Delete(ProfessionalAdvice ProfessionalAdvice) => _context.ProfessionalAdvices.Remove(ProfessionalAdvice);
        public void DeleteRange(List<ProfessionalAdvice> ProfessionalAdvices) => _context.ProfessionalAdvices.RemoveRange(ProfessionalAdvices);
        public void Update(ProfessionalAdvice ProfessionalAdvice) => _context.ProfessionalAdvices.Update(ProfessionalAdvice);

        public async Task<bool> ExistsAsync(ProfessionalAdviceId id) => await _context.ProfessionalAdvices
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<ProfessionalAdvice>> GetAll() => await _context.ProfessionalAdvices
            .AsNoTracking()
            .ToListAsync();

        public async Task<ProfessionalAdvice?> GetByIdAsync(ProfessionalAdviceId Id) =>
            await _context.ProfessionalAdvices
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<ProfessionalAdvice?> GetNoneProfessionalAdviceByCompanyIdAsync(CompanyId companyId) =>
            await _context.ProfessionalAdvices
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        public void AddRangeProfessionalAdvices(List<ProfessionalAdvice> ProfessionalAdvices) => _context.ProfessionalAdvices
            .AddRange(ProfessionalAdvices);

        public async Task<List<ProfessionalAdvice>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize)
        {
            if (page == 0 && pageSize == 0)
            {
                return await _context.ProfessionalAdvices.Include(c => c.Collaborators)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
            }
            else
            {
                return await _context.ProfessionalAdvices.Include(c => c.Collaborators)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetNumberOfProfessionalAdvices(CompanyId id) {
           List<ProfessionalAdvice>? amount =  await _context.ProfessionalAdvices.Where(p => p.CompanyId == id).ToListAsync();
           return amount.Count - 1;
        }
    }
}
