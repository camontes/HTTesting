using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.SearchFilters;
using HR_Platform.Domain.WorkplaceEvidences;
using HR_Platform.Domain.WorkplaceRecommendations;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class WorkplaceRecommendationRepository(ApplicationDbContext context) : IWorkplaceRecommendationRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(WorkplaceRecommendation WorkplaceRecommendation) => _context.WorkplaceRecommendations.Add(WorkplaceRecommendation);
        public void AddRange(List<WorkplaceRecommendation> WorkplaceRecommendations) => _context.WorkplaceRecommendations.AddRange(WorkplaceRecommendations);

        public void Delete(WorkplaceRecommendation WorkplaceRecommendation) => _context.WorkplaceRecommendations.Remove(WorkplaceRecommendation);
        public void DeleteRange(List<WorkplaceRecommendation> WorkplaceRecommendations) => _context.WorkplaceRecommendations.RemoveRange(WorkplaceRecommendations);
        public void Update(WorkplaceRecommendation WorkplaceRecommendation) => _context.WorkplaceRecommendations.Update(WorkplaceRecommendation);

        public async Task<bool> ExistsAsync(WorkplaceRecommendationId id) => await _context.WorkplaceRecommendations
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<WorkplaceRecommendation?> GetByIdAsync(WorkplaceRecommendationId Id) =>
            await _context.WorkplaceRecommendations
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);
               
        public async Task<List<WorkplaceRecommendation>?> GetByCollaboratorIdAsync(CollaboratorId collaboratorId, string year)
        {
            try
            {
                List<WorkplaceRecommendation> workplaceRecommendations = await _context.WorkplaceRecommendations
                .Where
                (
                    we
                    =>
                    we.CollaboratorId == collaboratorId
                )
                .ToListAsync();

                if (!string.IsNullOrEmpty(year))
                {
                    workplaceRecommendations = workplaceRecommendations.Where
                    (
                        we
                        =>
                        we.EditionDate.Value.Year.ToString() == year
                    ).ToList();
                }

                return workplaceRecommendations;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SearchFilter<WorkplaceRecommendation>> SearchAsync(BasicRequestSearch request)
        {
            var filter = _context.WorkplaceRecommendations
            .Include(x => x.Collaborator)
            .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.FileName.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)) && c.CollaboratorId == request.CollaboratorId).AsEnumerable();
            
            int year = request.Year is not null ? int.Parse(request.Year) : 0;

            var filterByYear = filter.Where(x => x.CreationDate.Value.Year == year);

            var baseQuery = request.Year is not null ? filterByYear : filter;

            var totalCount = baseQuery.Count();

            List<WorkplaceRecommendation> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

            return new SearchFilter<WorkplaceRecommendation>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
