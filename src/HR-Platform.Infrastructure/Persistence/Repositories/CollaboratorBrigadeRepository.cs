using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.CollaboratorBrigades;
using HR_Platform.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class CollaboratorBrigadeRepository(ApplicationDbContext context) : ICollaboratorBrigadeRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(CollaboratorBrigade CollaboratorBrigade) => _context.CollaboratorBrigades.Add(CollaboratorBrigade);

        public void Delete(CollaboratorBrigade CollaboratorBrigade) => _context.CollaboratorBrigades.Remove(CollaboratorBrigade);

        public void DeleteRange(List<CollaboratorBrigade> tags) => _context.CollaboratorBrigades.RemoveRange(tags);

        public void Update(CollaboratorBrigade CollaboratorBrigade) => _context.CollaboratorBrigades.Update(CollaboratorBrigade);
        public void UpdateRange(List<CollaboratorBrigade> CollaboratorBrigades) => _context.CollaboratorBrigades.UpdateRange(CollaboratorBrigades);

        public async Task<List<CollaboratorBrigade>> GetAll() =>
            await _context.CollaboratorBrigades
            .AsNoTracking()
            .Include(z => z.Collaborator)
            .ThenInclude(y => y.Assignation)
            .Include(q => q.BrigadeAdjustment)
            .Include(z => z.Collaborator)
            .ThenInclude(e => e.DocumentType)
            .Include(i => i.CollaboratorBrigadeInventory)
            .ThenInclude(o => o.CollaboratorBrigadeInventoryFiles)
            .Include(p => p.CollaboratorBrigadeInventory)
            .ThenInclude(a => a.BrigadeInventory)
            .Include(s => s.CollaboratorBrigadeInventory)
            .ThenInclude(d => d.UnitMeasure)
            .ToListAsync();

        public async Task<CollaboratorBrigade?> GetByIdAsync(CollaboratorBrigadeId Id) =>
            await _context.CollaboratorBrigades
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<CollaboratorBrigade>?> GetByCollaboratorBrigadeInventoryIdAsync(CollaboratorBrigadeInventoryId collaboratorBrigadeInventoryId) =>
                 await _context.CollaboratorBrigades
                    .Where(t => t.CollaboratorBrigadeInventoryId == collaboratorBrigadeInventoryId)
                    .ToListAsync();

        public void AddRangeCollaboratorBrigades(List<CollaboratorBrigade> CollaboratorBrigades) =>
            _context.CollaboratorBrigades
            .AddRange(CollaboratorBrigades);

        public async Task<SearchFilter<CollaboratorBrigade>> SearchAsync(BasicRequestSearch request)
        {
            var filter = _context.CollaboratorBrigades
                .AsNoTracking()
                .Include(z => z.Collaborator)
                .ThenInclude(y => y.Assignation)
                .Include(q => q.BrigadeAdjustment)
                .Include(z => z.Collaborator)
                .ThenInclude(e => e.DocumentType)
                .Include(i => i.CollaboratorBrigadeInventory)
                .ThenInclude(o => o.CollaboratorBrigadeInventoryFiles)
                .Include(p => p.CollaboratorBrigadeInventory)
                .ThenInclude(a => a.BrigadeInventory)
                .Include(s => s.CollaboratorBrigadeInventory)
                .ThenInclude(d => d.UnitMeasure)
                .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.Collaborator.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query))).AsEnumerable();
          
            int year = request.Year is not null ? int.Parse(request.Year) : 0;

            var filterByYear = filter.Where(x => x.CreationDate.Value.Year == year);

            var baseQuery = request.Year is not null ? filterByYear : filter;

            var totalCount = baseQuery.Count();


            List<CollaboratorBrigade> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

            return new SearchFilter<CollaboratorBrigade>
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<List<CollaboratorBrigade>> GetByBrigadeAdjustmentIdAsync(BrigadeAdjustmentId brigadeAdjustmentId) =>
            await _context.CollaboratorBrigades
                    .Where(t => t.BrigadeAdjustmentId == brigadeAdjustmentId)
                    .ToListAsync();
    }
}
