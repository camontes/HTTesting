using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.DreamMapAnswers;
using HR_Platform.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class CollaboratorDreamMapAnswerRepository(ApplicationDbContext context) : ICollaboratorDreamMapAnswerRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(CollaboratorDreamMapAnswer CollaboratorDreamMapAnswer) => _context.CollaboratorDreamMapAnswers.Add(CollaboratorDreamMapAnswer);

        public void Delete(CollaboratorDreamMapAnswer CollaboratorDreamMapAnswer) => _context.CollaboratorDreamMapAnswers.Remove(CollaboratorDreamMapAnswer);
        public void DeleteRange(List<CollaboratorDreamMapAnswer> CollaboratorDreamMapAnswer) => _context.CollaboratorDreamMapAnswers.RemoveRange(CollaboratorDreamMapAnswer);
        public void Update(CollaboratorDreamMapAnswer CollaboratorDreamMapAnswer) => _context.CollaboratorDreamMapAnswers.Update(CollaboratorDreamMapAnswer);
        public void UpdateRange(List<CollaboratorDreamMapAnswer> CollaboratorDreamMapAnswers) => _context.CollaboratorDreamMapAnswers.UpdateRange(CollaboratorDreamMapAnswers);

        public async Task<List<CollaboratorDreamMapAnswer>> GetAll() => await _context.CollaboratorDreamMapAnswers
            .Include(x => x.Collaborator)
            .ToListAsync();

        public async Task<List<CollaboratorDreamMapAnswer>> GetAllCollaborators() => await _context.CollaboratorDreamMapAnswers
            .AsNoTracking()
            .Include(x => x.Collaborator)
            .ThenInclude(y => y.Assignation)
            .Include(x => x.Collaborator)
            .ThenInclude(v => v.DocumentType)
            .GroupBy(x => x.Collaborator.Id)
            .Select(g => g.First())
            .ToListAsync();

        public async Task<CollaboratorDreamMapAnswer?> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
            await _context.CollaboratorDreamMapAnswers
            .Include(z => z.Collaborator)
            .Include(x => x.DreamMapAnswers)
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.CollaboratorId == collaboratorId);

        public void AddRangeCollaboratorDreamMapAnswers(List<CollaboratorDreamMapAnswer> CollaboratorDreamMapAnswer) => _context.CollaboratorDreamMapAnswers
            .AddRange(CollaboratorDreamMapAnswer);

        public async Task<CollaboratorDreamMapAnswer?> GetByIdAsync(CollaboratorDreamMapAnswerId CollaboratorDreamMapAnswerId) =>
            await _context.CollaboratorDreamMapAnswers.AsNoTracking().SingleOrDefaultAsync(c => c.Id == CollaboratorDreamMapAnswerId);

        public async Task<SearchFilter<CollaboratorDreamMapAnswer>> SearchAsync(BasicRequestSearch request)
        {
            IQueryable<CollaboratorDreamMapAnswer> baseQuery = _context.CollaboratorDreamMapAnswers
                .AsNoTracking()
                .Include(x => x.Collaborator)
                .ThenInclude(y => y.Assignation)
                .Include(x => x.Collaborator)
                .ThenInclude(v => v.DocumentType)
                .Where(c => DbFunctions.DbFunctions.RemoveAccents(c.Collaborator.Name.ToLower()).Contains(DbFunctions.DbFunctions.RemoveAccents(request.Query)));

            var totalCount = await baseQuery.CountAsync();
            List<CollaboratorDreamMapAnswer> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : await baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

            return new SearchFilter<CollaboratorDreamMapAnswer>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
