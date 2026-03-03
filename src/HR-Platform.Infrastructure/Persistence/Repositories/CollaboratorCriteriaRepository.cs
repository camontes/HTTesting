using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Positions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class CollaboratorCriteriaRepository(ApplicationDbContext context) : ICollaboratorCriteriaRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(CollaboratorCriteria CollaboratorCriteria) => _context.CollaboratorCriteria.Add(CollaboratorCriteria);

        public void AddRange(List<CollaboratorCriteria> CollaboratorCriteria) => _context.CollaboratorCriteria.AddRange(CollaboratorCriteria);

        public void Delete(CollaboratorCriteria CollaboratorCriteria) => _context.CollaboratorCriteria.Remove(CollaboratorCriteria);

        public async Task<List<CollaboratorCriteria>> GetAll() => await _context.CollaboratorCriteria.ToListAsync();

        public async Task<List<CollaboratorCriteria>?> GetByCollaboratorCriteriaIdAsync(CollaboratorCriteriaId id) =>
            await _context.CollaboratorCriteria.Where(x => x.Id == id).ToListAsync();

        public async Task<List<CollaboratorCriteria>?> GetByCollaboratorIdAndEvaluatorIdAsync(CollaboratorId collaboratorId, CollaboratorId evaluatorId) =>
            await _context.CollaboratorCriteria
            .AsNoTracking()
            .Include(p => p.Position)
            .Include(p => p.Evaluator)
            .Include(p => p.CollaboratorCriteriaAnswers)
            .ThenInclude(p => p.ImprovementPlans)
            .Where(x => x.CollaboratorEvaluatedId == collaboratorId && x.EvaluatorId == evaluatorId)
            .ToListAsync();

        public async Task<List<CollaboratorCriteria>?> GetByEvaluatorIdAsync(CollaboratorId id) =>
            await _context.CollaboratorCriteria
            .Include(x => x.CollaboratorEvaluated)
            .ThenInclude(e => e.Assignation)
            .Include(x => x.CollaboratorEvaluated)
            .ThenInclude(e => e.DocumentType)
            .Include(x => x.CollaboratorEvaluated)
            .ThenInclude(e => e.Position)
            .Where(x => x.EvaluatorId == id).ToListAsync();

        public async Task<CollaboratorCriteria?> GetByIdAsync(CollaboratorCriteriaId id) =>
            await _context.CollaboratorCriteria.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> IsExistingByCollaboratorIdAndEvaluatorIdAsync(CollaboratorId collaboratorId, CollaboratorId evaluatorId, PositionId positionId) =>
            await _context.CollaboratorCriteria.AnyAsync(x => x.CollaboratorEvaluatedId == collaboratorId && x.EvaluatorId == evaluatorId && x.PositionId == positionId);
       

        public void Update(CollaboratorCriteria CollaboratorCriteria) => _context.CollaboratorCriteria.Update(CollaboratorCriteria);
        public void UpdateRange(List<CollaboratorCriteria> CollaboratorCriterias) => _context.CollaboratorCriteria.UpdateRange(CollaboratorCriterias);
    }
}
