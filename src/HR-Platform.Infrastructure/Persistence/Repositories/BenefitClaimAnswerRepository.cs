using HR_Platform.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.BenefitClaimAnswers;
using HR_Platform.Domain.SearchFilters;
using HR_Platform.Domain.CollaboratorDreamMapAnswers;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class BenefitClaimAnswerRepository(ApplicationDbContext context) : IBenefitClaimAnswerRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(BenefitClaimAnswer benefitClaimAnswer) => _context.BenefitClaimAnswers.Add(benefitClaimAnswer);

        public async Task<List<BenefitClaimAnswer>?> GetByCompanyIdAsync(CompanyId companyId) =>
            await _context.BenefitClaimAnswers
                    .Include(x => x.Collaborator)
                    .ThenInclude(e => e.Assignation)
                    .Include(x => x.Collaborator)
                    .ThenInclude(e => e.DocumentType)
                    .Where(b => b.CompanyId == companyId)
                    .ToListAsync();
        public async Task<BenefitClaimAnswer?> GetByIdAsync(BenefitClaimAnswerId Id) =>
            await _context.BenefitClaimAnswers
            .AsNoTracking()
            .SingleOrDefaultAsync(b => b.Id == Id);

        public async Task<List<BenefitClaimAnswer>> GetByBenefitNameAsync(CompanyId companyId, string benefitName) =>
            await _context.BenefitClaimAnswers
                    .Include(x => x.Collaborator)
                    .Where(b => b.BenefitName == benefitName && b.CompanyId == companyId && b.IsBenefitAccepeted && !b.HasDeleted)
                    .ToListAsync();

        public async Task<List<BenefitClaimAnswer>> GetBenefitNamesAsync(CompanyId companyId) =>
            await _context.BenefitClaimAnswers
                    .Include(x => x.Collaborator)
                    .Where(b => b.CompanyId == companyId && b.IsBenefitAccepeted && !b.HasDeleted)
                    .ToListAsync();

        public void DeleteRange(List<BenefitClaimAnswer> benefitClaimAnswers) => _context.RemoveRange(benefitClaimAnswers);
        public void UpdateRange(List<BenefitClaimAnswer> benefitClaimAnswers) => _context.UpdateRange(benefitClaimAnswers);

        public async Task<SearchFilter<BenefitClaimAnswer>> SearchAsync(BasicRequestSearch request)
        {
            IQueryable<BenefitClaimAnswer> baseQuery = _context.BenefitClaimAnswers
            .Include(x => x.Collaborator)
            .ThenInclude(e => e.Assignation)
            .Include(x => x.Collaborator)
            .ThenInclude(e => e.DocumentType)
            .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.Collaborator.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)));

            var totalCount = await baseQuery.CountAsync();
            List<BenefitClaimAnswer> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : await baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

            return new SearchFilter<BenefitClaimAnswer>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
