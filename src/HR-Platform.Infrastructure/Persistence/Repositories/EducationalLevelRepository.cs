using HR_Platform.Domain.Companies;
using HR_Platform.Domain.EducationalLevels;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class EducationalLevelRepository(ApplicationDbContext context) : IEducationalLevelRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(EducationalLevel EducationalLevel) => _context.EducationalLevels.Add(EducationalLevel);

        public void Delete(EducationalLevel EducationalLevel) => _context.EducationalLevels.Remove(EducationalLevel);
        public void DeleteRange(List<EducationalLevel> EducationalLevels) => _context.EducationalLevels.RemoveRange(EducationalLevels);
        public void Update(EducationalLevel EducationalLevel) => _context.EducationalLevels.Update(EducationalLevel);

        public async Task<bool> ExistsAsync(EducationalLevelId id) => await _context.EducationalLevels
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<EducationalLevel>> GetAll() => await _context.EducationalLevels
            .AsNoTracking()
            .ToListAsync();

        public async Task<EducationalLevel?> GetByIdAsync(EducationalLevelId Id) =>
            await _context.EducationalLevels
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<EducationalLevel?> GetNoneEducationalLevelByCompanyIdAsync(CompanyId companyId) =>
            await _context.EducationalLevels
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        public void AddRangeEducationalLevels(List<EducationalLevel> EducationalLevels) => _context.EducationalLevels
            .AddRange(EducationalLevels);

        public async Task<List<EducationalLevel>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize)
        {
            if (page == 0 && pageSize == 0)
            {
                return await _context.EducationalLevels.Include(c => c.Collaborators)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();
            }
            else
            {
                return await _context.EducationalLevels.Include(c => c.Collaborators)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetNumberOfEducationalLevels(CompanyId id) {
           List<EducationalLevel>? amount =  await _context.EducationalLevels.Where(p => p.CompanyId == id).ToListAsync();
           return amount.Count - 1;
        }
    }
}
