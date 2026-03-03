using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;
using HR_Platform.Domain.TalentPools;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class TalentPoolRepository(ApplicationDbContext context) : ITalentPoolRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(TalentPool TalentPool) => _context.TalentPool.Add(TalentPool);

        public void Delete(TalentPool TalentPool) => _context.TalentPool.Remove(TalentPool);
        public void DeleteRange(List<TalentPool> TalentPool) => _context.TalentPool.RemoveRange(TalentPool);
        public void Update(TalentPool TalentPool) => _context.TalentPool.Update(TalentPool);

        public async Task<List<TalentPool>> GetAll() => await _context.TalentPool
            .AsNoTracking()
            .ToListAsync();

        public async Task<TalentPool?> GetByIdAsync(TalentPoolId Id) =>
            await _context.TalentPool
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public void AddRangeTalentPools(List<TalentPool> TalentPool) => _context.TalentPool
            .AddRange(TalentPool);


        public async Task<List<TalentPool>?> GetByCompanyIdAsync(CompanyId companyId, int page, int pageSize)
        {
            if (page == 0 && pageSize == 0)
            {
                return await _context.TalentPool.Include(c => c.CollaboratorTalentPools)
                    .Where(h => h.CompanyId == companyId)
                    .ToListAsync();
            }
            else
            {
                return await _context.TalentPool.Include(c => c.CollaboratorTalentPools)
                    .Where(h => h.CompanyId == companyId)
                    .Skip((page) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetNumberOfTalentPools(CompanyId id) {
            List<TalentPool>? amount = await _context.TalentPool.Where(p => p.CompanyId == id).ToListAsync();
            return amount.Count;
        }

        public async Task<SearchFilter<TalentPool>> SearchAsync(BasicRequestSearch request)
        {
            bool IsTalentPoolArchived = request.IsTalentPoolArchived is not null;
            IQueryable<TalentPool> baseQuery;

            if (IsTalentPoolArchived)
            {
                baseQuery = _context.TalentPool
                .Include(c => c.CollaboratorTalentPools)
                .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.Tittle.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)) && c.IsArchived);
            }
            else
            {
                baseQuery = _context.TalentPool
                 .Include(c => c.CollaboratorTalentPools)
                 .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.Tittle.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)));
            }
            var totalCount = await baseQuery.CountAsync();
            List<TalentPool> items = request.Page == 0 || request.PageSize == 0
            ? [.. baseQuery]
            : await baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

            return new SearchFilter<TalentPool>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
