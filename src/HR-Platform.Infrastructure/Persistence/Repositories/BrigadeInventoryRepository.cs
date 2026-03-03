using HR_Platform.Domain.BrigadeDocumentations;
using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class BrigadeInventoryRepository(ApplicationDbContext context) : IBrigadeInventoryRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(BrigadeInventory BrigadeInventory) => _context.BrigadeInventories.Add(BrigadeInventory);
        public void AddRange(List<BrigadeInventory> BrigadeInventories) => _context.BrigadeInventories.AddRange(BrigadeInventories);

        public void Delete(BrigadeInventory BrigadeInventory) => _context.BrigadeInventories.Remove(BrigadeInventory);
        public void Update(BrigadeInventory BrigadeInventory) => _context.BrigadeInventories.Update(BrigadeInventory);

        public async Task<bool> ExistsAsync(BrigadeInventoryId id) => await _context.BrigadeInventories
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<BrigadeInventory?> GetByIdAsync(BrigadeInventoryId Id) =>
            await _context.BrigadeInventories
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<BrigadeInventory?> GetNoneBrigadeInventoryByCompanyIdAsync(CompanyId companyId) =>
            await _context.BrigadeInventories
            .Where(p => p.CompanyId == companyId && p.Name == "Ninguno")
            .FirstOrDefaultAsync();

        public async Task<List<BrigadeInventory>?> GetByCompanyIdAsync(CompanyId companyId, string year)
        {
            try
            {
                List<BrigadeInventory> brigades = await _context.BrigadeInventories
                    .Include(bi => bi.UnitMeasure)
                    .Include(bi => bi.BrigadeInventoryFiles)
                    .Where
                    (
                        bi
                        =>
                        bi.CompanyId == companyId
                        &&
                        bi.Name != "Ninguno"
                        &&
                        !bi.IsDeleted
                     )
                    .ToListAsync();

                if (!string.IsNullOrEmpty(year))
                {
                    brigades = brigades.Where
                    (
                        bi
                        =>
                        bi.EditionDate.Value.Year.ToString() == year
                    ).ToList();
                }

                return brigades;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<SearchFilter<BrigadeInventory>> SearchAsync(BasicRequestSearch request)
        {
            var filter = _context.BrigadeInventories
                 .AsNoTracking()
                 .Include(x => x.UnitMeasure)
                 .Include(z => z.BrigadeInventoryFiles)
                 .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)) && c.Name != "Ninguno" && !c.IsDeleted).AsEnumerable();

            int year = request.Year is not null ? int.Parse(request.Year) : 0;

            var filterByYear = filter.Where(x => x.CreationDate.Value.Year == year);

            var baseQuery = request.Year is not null ? filterByYear : filter;

            var totalCount = baseQuery.Count();

            List<BrigadeInventory> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

            return new SearchFilter<BrigadeInventory>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
