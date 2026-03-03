using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.ImprovementPlanTasks;
using HR_Platform.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class ImprovementPlanTaskRepository(ApplicationDbContext context) : IImprovementPlanTaskRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        public void Add(ImprovementPlanTask improvementPlanTask) => _context.ImprovementPlanTasks.Add(improvementPlanTask);
        public void AddRange(List<ImprovementPlanTask> improvementPlanTasks) => _context.ImprovementPlanTasks.AddRange(improvementPlanTasks);

        public void Delete(ImprovementPlanTask improvementPlanTask) => _context.ImprovementPlanTasks.Remove(improvementPlanTask);
        public void Update(ImprovementPlanTask improvementPlanTask) => _context.ImprovementPlanTasks.Update(improvementPlanTask);
        public async Task<bool> ExistsAsync(ImprovementPlanTaskId id) => await _context.ImprovementPlanTasks
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);
        public async Task<ImprovementPlanTask?> GetByIdAsync(ImprovementPlanTaskId Id) =>
            await _context.ImprovementPlanTasks
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);
        public async Task<List<ImprovementPlanTask>?> GetByCollaboratorCriteriaAnswerIdAsync(CollaboratorCriteriaAnswerId collaboratorCriteriaAnswerId) =>
            await _context.ImprovementPlanTasks
                    .Include(x => x.ImprovementPlanTaskFiles)
                    .Include(x => x.ImprovementPlan)
                    .Where(h => h.ImprovementPlan.CollaboratorCriteriaAnswerId == collaboratorCriteriaAnswerId)
                    .ToListAsync();

        public async Task<List<ImprovementPlanTask>?> GetByEvaluatorIdAsync(CollaboratorId evaluatorId) =>
            await _context.ImprovementPlanTasks
                    .Include(ip => ip.ImprovementPlanTaskFiles)
                    .Include(ip => ip.ImprovementPlan)
                    .Include(ip => ip.ImprovementPlan.CollaboratorCriteriaAnswer)
                    .ThenInclude(eca => eca.CollaboratorCriteria)
                    .ThenInclude(ec => ec.Evaluator)
                    .Include(ip => ip.ImprovementPlan.CollaboratorCriteriaAnswer)
                    .ThenInclude(eca => eca.CollaboratorCriteria)
                    .ThenInclude(ec => ec.CollaboratorEvaluated)
                    .Where(ip => ip.ImprovementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == evaluatorId)
                    .ToListAsync();

        public async Task<SearchFilter<ImprovementPlanTask>> GetByEvaluatorIdAndNameAsync(ImprovementPlansRequestSearch request)
        {
            IQueryable<ImprovementPlanTask> baseQuery;

            if (string.IsNullOrEmpty(request.CollaboratorName))
            {
                baseQuery = _context.ImprovementPlanTasks
                    .AsNoTracking()
                    .Include(ip => ip.ImprovementPlanTaskFiles)
                    .Include(ip => ip.ImprovementPlan)
                    .Include(ip => ip.ImprovementPlan.CollaboratorCriteriaAnswer)
                    .ThenInclude(eca => eca.CollaboratorCriteria)
                    .ThenInclude(ec => ec.Evaluator)
                    .Include(ip => ip.ImprovementPlan.CollaboratorCriteriaAnswer)
                    .ThenInclude(eca => eca.CollaboratorCriteria)
                    .ThenInclude(ec => ec.CollaboratorEvaluated)
                    .Where(ip => ip.ImprovementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId));
            }
            else
            {
                baseQuery = _context.ImprovementPlanTasks
                    .AsNoTracking()
                    .Include(ip => ip.ImprovementPlanTaskFiles)
                    .Include(ip => ip.ImprovementPlan)
                    .Include(ip => ip.ImprovementPlan.CollaboratorCriteriaAnswer)
                    .ThenInclude(eca => eca.CollaboratorCriteria)
                    .ThenInclude(ec => ec.Evaluator)
                    .Include(ip => ip.ImprovementPlan.CollaboratorCriteriaAnswer)
                    .ThenInclude(eca => eca.CollaboratorCriteria)
                    .ThenInclude(ec => ec.CollaboratorEvaluated)
                    .Where
                    (
                        ip
                        =>
                        ip.ImprovementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId)
                        &&
                        DbFunctions.DbFunctions.RemoveAccents
                        (
                            ip.ImprovementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Name.ToLower()
                        )
                        .Contains(DbFunctions.DbFunctions.RemoveAccents(request.CollaboratorName))
                        &&
                        ip.ImprovementPlan.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId)
                    );
            }

            int totalCount = await baseQuery.CountAsync();
            List<ImprovementPlanTask> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : await baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

            return new SearchFilter<ImprovementPlanTask>
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<int> CountTasksByCollaboratorAsync(CollaboratorCriteriaAnswerId CollaboratorCriteriaAnswerId) =>
            await _context.ImprovementPlanTasks
                    .Include(ip => ip.ImprovementPlanTaskFiles)
                    .Include(ip => ip.ImprovementPlan)
                    .Include(ip => ip.ImprovementPlan.CollaboratorCriteriaAnswer)
                    .Where(ip => ip.ImprovementPlan.CollaboratorCriteriaAnswerId == CollaboratorCriteriaAnswerId)
                    .CountAsync();
    }
}
