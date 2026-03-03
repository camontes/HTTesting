using HR_Platform.Domain.BenefitClaimAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.OccupationalRecommendations;
using HR_Platform.Domain.SearchFilters;
using HR_Platform.Domain.WorkplaceEvidences;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class WorkplaceEvidenceRepository(ApplicationDbContext context) : IWorkplaceEvidenceRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(WorkplaceEvidence WorkplaceEvidence) => _context.WorkplaceEvidences.Add(WorkplaceEvidence);
        public void AddRange(List<WorkplaceEvidence> WorkplaceEvidences) => _context.WorkplaceEvidences.AddRange(WorkplaceEvidences);

        public void Delete(WorkplaceEvidence WorkplaceEvidence) => _context.WorkplaceEvidences.Remove(WorkplaceEvidence);
        public void DeleteRange(List<WorkplaceEvidence> WorkplaceEvidences) => _context.WorkplaceEvidences.RemoveRange(WorkplaceEvidences);
        public void Update(WorkplaceEvidence WorkplaceEvidence) => _context.WorkplaceEvidences.Update(WorkplaceEvidence);

        public async Task<bool> ExistsAsync(WorkplaceEvidenceId id) => await _context.WorkplaceEvidences
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<WorkplaceEvidence?> GetByIdAsync(WorkplaceEvidenceId Id) =>
            await _context.WorkplaceEvidences
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<WorkplaceEvidence>?> GetByCollaboratorIdAsync(CollaboratorId collaboratorId, string year)
        {
            try
            {
                List<WorkplaceEvidence> workplaceEvidences = await _context.WorkplaceEvidences
                .Where
                (
                    we
                    =>
                    we.CollaboratorId == collaboratorId
                )
                .ToListAsync();

                if (!string.IsNullOrEmpty(year))
                {
                    workplaceEvidences = workplaceEvidences.Where
                    (
                        we
                        =>
                        we.EditionDate.Value.Year.ToString() == year
                    ).ToList();
                }

                return workplaceEvidences;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SearchFilter<WorkplaceEvidence>> SearchAsync(BasicRequestSearch request)
        {
            var filter = _context.WorkplaceEvidences
            .Include(x => x.Collaborator)
            .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.FileName.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)) && c.CollaboratorId == request.CollaboratorId).AsEnumerable();

            int year = request.Year is not null ? int.Parse(request.Year) : 0;

            var filterByYear = filter.Where(x => x.CreationDate.Value.Year == year);

            var baseQuery = request.Year is not null ? filterByYear : filter;

            var totalCount = baseQuery.Count();

            List<WorkplaceEvidence> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

            return new SearchFilter<WorkplaceEvidence>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
