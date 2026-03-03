using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.OccupationalRecommendations;
using HR_Platform.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class OccupationalRecommendationRepository(ApplicationDbContext context) : IOccupationalRecommendationRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(OccupationalRecommendation OccupationalRecommendation) => _context.OccupationalRecommendations.Add(OccupationalRecommendation);
        public void AddRange(List<OccupationalRecommendation> OccupationalRecommendations) => _context.OccupationalRecommendations.AddRange(OccupationalRecommendations);

        public void Delete(OccupationalRecommendation OccupationalRecommendation) => _context.OccupationalRecommendations.Remove(OccupationalRecommendation);
        public void DeleteRange(List<OccupationalRecommendation> OccupationalRecommendations) => _context.OccupationalRecommendations.RemoveRange(OccupationalRecommendations);
        public void Update(OccupationalRecommendation OccupationalRecommendation) => _context.OccupationalRecommendations.Update(OccupationalRecommendation);

        public async Task<bool> ExistsAsync(OccupationalRecommendationId id) => await _context.OccupationalRecommendations
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<OccupationalRecommendation?> GetByIdAsync(OccupationalRecommendationId Id) =>
            await _context.OccupationalRecommendations
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<OccupationalRecommendation>?> GetByCollaboratorIdAsync(CollaboratorId collaboratorId, string year)
        {
            try
            {
                List<OccupationalRecommendation> recommendations = await _context.OccupationalRecommendations
                .Where
                (
                    or
                    =>
                    or.CollaboratorId == collaboratorId
                )
                .ToListAsync();

                if (!string.IsNullOrEmpty(year))
                {
                    recommendations = recommendations.Where
                    (
                        or
                        =>
                        or.EditionDate.Value.Year.ToString() == year
                    ).ToList();
                }

                return recommendations;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SearchFilter<OccupationalRecommendation>> SearchAsync(BasicRequestSearch request)
        {
            var filter = _context.OccupationalRecommendations
            .Include(x => x.Collaborator)
            .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.FileName.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)) && c.CollaboratorId == request.CollaboratorId).AsEnumerable();

            int year = request.Year is not null ? int.Parse(request.Year) : 0;

            var filterByYear = filter.Where(x => x.CreationDate.Value.Year == year);

            var baseQuery = request.Year is not null ? filterByYear : filter;

            var totalCount = baseQuery.Count();

            List<OccupationalRecommendation> items = request.Page == 0 || request.PageSize == 0
            ? [.. baseQuery]
            : baseQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new SearchFilter<OccupationalRecommendation>
            {
            TotalCount = totalCount,
            Items = items
            };
        }
    }
}
