using HR_Platform.Domain.Benefits;
using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Companies;
using HR_Platform.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class CollaboratorBenefitClaimRepository(ApplicationDbContext context) : ICollaboratorBenefitClaimRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(CollaboratorBenefitClaim CollaboratorBenefitClaim) => _context.CollaboratorBenefitClaims.Add(CollaboratorBenefitClaim);

        public void Delete(CollaboratorBenefitClaim CollaboratorBenefitClaim) => _context.CollaboratorBenefitClaims.Remove(CollaboratorBenefitClaim);
        public void Update(CollaboratorBenefitClaim CollaboratorBenefitClaim) => _context.CollaboratorBenefitClaims.Update(CollaboratorBenefitClaim);

        public async Task<bool> ExistsAsync(CollaboratorBenefitClaimId id) => await _context.CollaboratorBenefitClaims
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<CollaboratorBenefitClaim?> GetByIdAsync(CollaboratorBenefitClaimId Id) =>
            await _context.CollaboratorBenefitClaims.Include(c => c.Collaborator).ThenInclude(x => x.DocumentType).Include(c => c.Benefit)
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<CollaboratorBenefitClaim>?> GetByCompanyIdAsync(CompanyId companyId) =>
         await _context.CollaboratorBenefitClaims
            .Include(c => c.Collaborator)
            .ThenInclude(x => x.DocumentType)
            .Include(c => c.Benefit)
            .Include(c => c.Collaborator)
            .ThenInclude(x => x.Assignation)
            .Where(h => h.CompanyId == companyId)
            .ToListAsync();

        public async Task<List<CollaboratorBenefitClaim>?> GetByBenefitIdAsync(BenefitId benefitId) =>
         await _context.CollaboratorBenefitClaims
            .Where(cb => cb.BenefitId == benefitId)
            .ToListAsync();

        public async Task<int> GetNumberOfCollaboratorBenefitClaims(CompanyId id)
        {
            List<CollaboratorBenefitClaim>? amount = await _context.CollaboratorBenefitClaims.Where(p => p.CompanyId == id).ToListAsync();
            return amount.Count - 1;
        }

        public void AddRangeCollaboratorBenefitClaims(List<CollaboratorBenefitClaim> contract) =>
             _context.CollaboratorBenefitClaims.AddRange(contract);

        public async Task<CollaboratorBenefitClaim?> ValidateClaimAsync(BenefitId benefitId, CollaboratorId collaboratorId) =>
            await _context.CollaboratorBenefitClaims.SingleOrDefaultAsync(x => x.BenefitId == benefitId && x.CollaboratorId == collaboratorId);

        public async Task<SearchFilter<CollaboratorBenefitClaim>> SearchAsync(BasicRequestSearch request)
        {
            IQueryable<CollaboratorBenefitClaim> baseQuery = _context.CollaboratorBenefitClaims
                .Include(c => c.Collaborator)
                .ThenInclude(x => x.DocumentType)
                .Include(c => c.Benefit)
                .Include(c => c.Collaborator)
                .ThenInclude(x => x.Assignation)
                .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.Collaborator.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)));

            var totalCount = await baseQuery.CountAsync();
            List<CollaboratorBenefitClaim> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : await baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

            return new SearchFilter<CollaboratorBenefitClaim>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
