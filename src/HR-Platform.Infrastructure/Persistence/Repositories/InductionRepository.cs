using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class InductionRepository(ApplicationDbContext context) : IInductionRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Induction Induction) => _context.Induction.Add(Induction);
        public void AddRange(List<Induction> Induction) => _context.Induction.AddRange(Induction);

        public void Delete(Induction Induction) => _context.Induction.Remove(Induction);
        public void Update(Induction Induction) => _context.Induction.Update(Induction);

        public async Task<bool> ExistsAsync(InductionId id) => await _context.Induction
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<Induction?> GetByIdAsync(InductionId Id) =>
            await _context.Induction
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<Induction?> GetNoneInductionByCompanyIdAsync(CompanyId companyId) =>
            await _context.Induction
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        
        public async Task<List<Induction>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.Induction.Include(x => x.InductionFiles)
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();

        public async Task<SearchFilter<Induction>> SearchAsync(BasicRequestSearch request)
        {
            var filter = _context.Induction
                 .AsNoTracking()
                 .Include(i => i.InductionFiles)
                 .Where(i => DbFunctions.DbFunctions.RemoveAccents(i.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)) && i.Name != "Ninguno").AsEnumerable();

            var baseQuery = filter;

            var totalCount = baseQuery.Count();

            List<Induction> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

            return new SearchFilter<Induction>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
