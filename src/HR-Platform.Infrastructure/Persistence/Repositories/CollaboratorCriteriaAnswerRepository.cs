using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.Collaborators;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class CollaboratorCriteriaAnswerRepository(ApplicationDbContext context) : ICollaboratorCriteriaAnswerRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(CollaboratorCriteriaAnswer CollaboratorCriteriaAnswer) => _context.CollaboratorCriteriaAnswer.Add(CollaboratorCriteriaAnswer);

        public void AddRange(List<CollaboratorCriteriaAnswer> CollaboratorCriteriaAnswer) => _context.CollaboratorCriteriaAnswer.AddRange(CollaboratorCriteriaAnswer);

        public void Delete(CollaboratorCriteriaAnswer CollaboratorCriteriaAnswer) => _context.CollaboratorCriteriaAnswer.Remove(CollaboratorCriteriaAnswer);

        public async Task<List<CollaboratorCriteriaAnswer>> GetAll() => await _context.CollaboratorCriteriaAnswer.AsNoTracking().ToListAsync();
        public async Task<List<CollaboratorCriteriaAnswer>> GetAllWithoutHistorical(CollaboratorId collaboratorId) => await _context.CollaboratorCriteriaAnswer
            .Include(y => y.CollaboratorCriteria)
            .Where(x => !x.IsInHistorical && x.CollaboratorCriteria.EvaluatorId == collaboratorId)
            .ToListAsync();

        public async Task<List<CollaboratorCriteriaAnswer>?> GetByCollaboratorCriteriaAnswerIdAsync(CollaboratorCriteriaAnswerId id) =>
            await _context.CollaboratorCriteriaAnswer.Where(x => x.Id == id).ToListAsync();

        public async Task<CollaboratorCriteriaAnswer?> GetByIdAsync(CollaboratorCriteriaAnswerId id) =>
            await _context.CollaboratorCriteriaAnswer.SingleOrDefaultAsync(x => x.Id == id);

        public void Update(CollaboratorCriteriaAnswer CollaboratorCriteriaAnswer) => _context.CollaboratorCriteriaAnswer.Update(CollaboratorCriteriaAnswer);
        public void UpdateRange(List<CollaboratorCriteriaAnswer> CollaboratorCriteriaAnswers) => _context.CollaboratorCriteriaAnswer.UpdateRange(CollaboratorCriteriaAnswers);
    }
}
