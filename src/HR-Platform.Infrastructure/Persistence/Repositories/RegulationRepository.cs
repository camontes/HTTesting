using HR_Platform.Domain.BenefitClaimAnswers;
using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.Regulations;
using HR_Platform.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class RegulationRepository(ApplicationDbContext context) : IRegulationRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Regulation Regulation) => _context.Regulations.Add(Regulation);
        public void AddRange(List<Regulation> Regulations) => _context.Regulations.AddRange(Regulations);

        public void Delete(Regulation Regulation) => _context.Regulations.Remove(Regulation);
        public void Update(Regulation Regulation) => _context.Regulations.Update(Regulation);

        public async Task<bool> ExistsAsync(RegulationId id) => await _context.Regulations
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<Regulation?> GetByIdAsync(RegulationId Id) =>
            await _context.Regulations
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<Regulation?> GetNoneRegulationByCompanyIdAsync(CompanyId companyId) =>
            await _context.Regulations
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();

        public async Task<List<Regulation>?> GetByCompanyIdAsync(CompanyId companyId, string year)
        {
            try
            {
                List<Regulation> regulations = await _context.Regulations
                    .Where
                    (
                        r
                        =>
                        r.CompanyId == companyId
                        &&
                        r.Name != "Ninguno"
                     )
                    .ToListAsync();

                if (!string.IsNullOrEmpty(year))
                {
                    regulations = regulations.Where
                    (
                        r
                        =>
                        r.EditionDate.Value.Year.ToString() == year
                    ).ToList();
                }

                return regulations;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SearchFilter<Regulation>> SearchAsync(BasicRequestSearch request)
        {
            var filter = _context.Regulations
            .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.FileName.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)) && c.Name != "Ninguno").AsEnumerable();
            
            int year = request.Year is not null ? int.Parse(request.Year) : 0;

            var filterByYear = filter.Where(x => x.CreationDate.Value.Year == year);

            var baseQuery = request.Year is not null ? filterByYear : filter;

            var totalCount = baseQuery.Count();

            List<Regulation> items = request.Page == 0 || request.PageSize == 0
            ? [.. baseQuery]
            : baseQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

            return new SearchFilter<Regulation>
            {
            TotalCount = totalCount,
            Items = items
            };
        }
    }
}
