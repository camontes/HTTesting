using HR_Platform.Domain.Companies;
using HR_Platform.Domain.BrigadeDocumentations;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.SearchFilters;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class BrigadeDocumentationRepository(ApplicationDbContext context) : IBrigadeDocumentationRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(BrigadeDocumentation BrigadeDocumentation) => _context.BrigadeDocumentations.Add(BrigadeDocumentation);
        public void AddRange(List<BrigadeDocumentation> BrigadeDocumentations) => _context.BrigadeDocumentations.AddRange(BrigadeDocumentations);

        public void Delete(BrigadeDocumentation BrigadeDocumentation) => _context.BrigadeDocumentations.Remove(BrigadeDocumentation);
        public void Update(BrigadeDocumentation BrigadeDocumentation) => _context.BrigadeDocumentations.Update(BrigadeDocumentation);

        public async Task<bool> ExistsAsync(BrigadeDocumentationId id) => await _context.BrigadeDocumentations
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<BrigadeDocumentation?> GetByIdAsync(BrigadeDocumentationId Id) =>
            await _context.BrigadeDocumentations
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<BrigadeDocumentation?> GetNoneBrigadeDocumentationByCompanyIdAsync(CompanyId companyId) =>
            await _context.BrigadeDocumentations
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();
        
        public async Task<List<BrigadeDocumentation>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.BrigadeDocumentations
                    .Where(h => h.CompanyId == companyId && h.Name != "Ninguno")
                    .ToListAsync();

        public async Task<SearchFilter<BrigadeDocumentation>> SearchAsync(BasicRequestSearch request)
        {
            var filter = _context.BrigadeDocumentations
                .AsNoTracking()
                .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.FileName.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)) && c.Name != "Ninguno").AsEnumerable();

            int year = request.Year is not null ? int.Parse(request.Year) : 0;

            var filterByYear = filter.Where(x => x.CreationDate.Value.Year == year);

            var baseQuery = request.Year is not null ? filterByYear : filter;

            var totalCount = baseQuery.Count();

            List<BrigadeDocumentation> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

            return new SearchFilter<BrigadeDocumentation>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
