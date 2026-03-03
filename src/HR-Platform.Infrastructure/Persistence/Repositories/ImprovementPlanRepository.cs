using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.ImprovementPlans;
using HR_Platform.Domain.ImprovementPlanTasks;
using HR_Platform.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class ImprovementPlanRepository(ApplicationDbContext context) : IImprovementPlanRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        public void Add(ImprovementPlan improvementPlan) => _context.ImprovementPlans.Add(improvementPlan);

        public async Task<ImprovementPlan?> GetByIdAsync(ImprovementPlanId Id) =>
            await _context.ImprovementPlans
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<SearchFilter<ImprovementPlan>> GetByEvaluatorIdAndNameAsync(ImprovementPlansRequestSearch request)
        {
            IQueryable<ImprovementPlan>? baseQuery = null;

            switch(request.WithResponses)
            {
                case 0:
                    if (string.IsNullOrEmpty(request.CollaboratorName))
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where(ip => ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId));
                    }
                    else
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where
                            (
                                ip
                                =>
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId)
                                &&
                                DbFunctions.DbFunctions.RemoveAccents
                                (
                                    ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Name.ToLower()
                                )
                                .Contains(DbFunctions.DbFunctions.RemoveAccents(request.CollaboratorName.ToLower()))
                                &&
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId)
                            );
                    }

                    break;
                case 1:
                    if (string.IsNullOrEmpty(request.CollaboratorName))
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanResponse)
                            .ThenInclude(ipr => ipr.ImprovementPlanResponseFiles)
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where
                            (
                                ip
                                =>
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId)
                                &&
                                ip.ImprovementPlanTasks.Any
                                (
                                    ipt
                                    =>
                                    ipt.ImprovementPlanResponse != null
                                    &&
                                    ipt.ImprovementPlanResponse.Count > 0
                                    &&
                                    !string.IsNullOrEmpty(ipt.ImprovementPlanResponse[0].TaskResponse)
                                )
                            );
                    }
                    else
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanResponse)
                            .ThenInclude(ipr => ipr.ImprovementPlanResponseFiles)
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where
                            (
                                ip
                                =>
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId)
                                &&
                                DbFunctions.DbFunctions.RemoveAccents
                                (
                                    ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Name.ToLower()
                                )
                                .Contains(DbFunctions.DbFunctions.RemoveAccents(request.CollaboratorName.ToLower()))
                                &&
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId)
                                &&
                                ip.ImprovementPlanTasks.Any
                                (
                                    ipt
                                    =>
                                    ipt.ImprovementPlanResponse != null
                                    &&
                                    ipt.ImprovementPlanResponse.Count > 0
                                    &&
                                    !string.IsNullOrEmpty(ipt.ImprovementPlanResponse[0].TaskResponse)
                                )
                            );
                    }

                    break;
                case 2:
                    if (string.IsNullOrEmpty(request.CollaboratorName))
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanResponse)
                            .ThenInclude(ipr => ipr.ImprovementPlanResponseFiles)
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where
                            (
                                ip
                                =>
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId)
                                &&
                                ip.ImprovementPlanTasks.All
                                (
                                    ipt
                                    =>
                                    ipt.ImprovementPlanResponse == null
                                    ||
                                    ipt.ImprovementPlanResponse.Count == 0
                                    ||
                                    string.IsNullOrEmpty(ipt.ImprovementPlanResponse[0].TaskResponse)
                                )
                            );
                    }
                    else
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanResponse)
                            .ThenInclude(ipr => ipr.ImprovementPlanResponseFiles)
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where
                            (
                                ip
                                =>
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId)
                                &&
                                DbFunctions.DbFunctions.RemoveAccents
                                (
                                    ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Name.ToLower()
                                )
                                .Contains(DbFunctions.DbFunctions.RemoveAccents(request.CollaboratorName.ToLower()))
                                &&
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId)
                                &&
                                ip.ImprovementPlanTasks.All
                                (
                                    ipt
                                    =>
                                    ipt.ImprovementPlanResponse == null
                                    ||
                                    ipt.ImprovementPlanResponse.Count == 0
                                    ||
                                    string.IsNullOrEmpty(ipt.ImprovementPlanResponse[0].TaskResponse)
                                )
                            );
                    }

                    break;
                default:
                    break;
            };

            int totalCount = await baseQuery.CountAsync();
            List<ImprovementPlan> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : await baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

            return new SearchFilter<ImprovementPlan>
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<SearchFilter<ImprovementPlan>> GetByCollaboratorIdAndNameAsync(ImprovementPlansRequestSearch request)
        {           
            IQueryable<ImprovementPlan>? baseQuery = null;

            switch (request.WithResponses)
            {
                case 0:
                    if (string.IsNullOrEmpty(request.CollaboratorName))
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanResponse)
                            .ThenInclude(iptr => iptr.ImprovementPlanResponseFiles)
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where(ip => ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluatedId == request.CollaboratorId);
                    }
                    else
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanResponse)
                            .ThenInclude(iptr => iptr.ImprovementPlanResponseFiles)
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where
                            (
                                ip
                                =>
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.EvaluatorId == new CollaboratorId(request.EvaluatorId)
                                &&
                                DbFunctions.DbFunctions.RemoveAccents
                                (
                                    ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Name.ToLower()
                                )
                                .Contains(DbFunctions.DbFunctions.RemoveAccents(request.CollaboratorName.ToLower()))
                                &&
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluatedId == request.CollaboratorId
                            );
                    }

                    break;
                case 1:
                    if (string.IsNullOrEmpty(request.CollaboratorName))
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanResponse)
                            .ThenInclude(iptr => iptr.ImprovementPlanResponseFiles)
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where
                            (
                                ip
                                =>
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluatedId == request.CollaboratorId
                                &&
                                ip.ImprovementPlanTasks.Any
                                (
                                    ipt
                                    =>
                                    ipt.ImprovementPlanResponse != null
                                    &&
                                    ipt.ImprovementPlanResponse.Count > 0
                                    &&
                                    !string.IsNullOrEmpty(ipt.ImprovementPlanResponse[0].TaskResponse)
                                )
                            );
                    }
                    else
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanResponse)
                            .ThenInclude(iptr => iptr.ImprovementPlanResponseFiles)
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where
                            (
                                ip
                                =>
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluatedId == request.CollaboratorId
                                &&
                                DbFunctions.DbFunctions.RemoveAccents
                                (
                                    ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Name.ToLower()
                                )
                                .Contains(DbFunctions.DbFunctions.RemoveAccents(request.CollaboratorName.ToLower()))
                                &&
                                ip.ImprovementPlanTasks.Any
                                (
                                    ipt
                                    =>
                                    ipt.ImprovementPlanResponse != null
                                    &&
                                    ipt.ImprovementPlanResponse.Count > 0
                                    &&
                                    !string.IsNullOrEmpty(ipt.ImprovementPlanResponse[0].TaskResponse)
                                )
                            );
                    }

                    break;
                case 2:
                    if (string.IsNullOrEmpty(request.CollaboratorName))
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanResponse)
                            .ThenInclude(iptr => iptr.ImprovementPlanResponseFiles)
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where
                            (
                                ip
                                =>
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluatedId == request.CollaboratorId
                                &&
                                ip.ImprovementPlanTasks.All
                                (
                                    ipt
                                    =>
                                    ipt.ImprovementPlanResponse == null
                                    ||
                                    ipt.ImprovementPlanResponse.Count == 0
                                    ||
                                    string.IsNullOrEmpty(ipt.ImprovementPlanResponse[0].TaskResponse)
                                )
                            );
                    }
                    else
                    {
                        baseQuery = _context.ImprovementPlans
                            .AsNoTracking()
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanResponse)
                            .ThenInclude(iptr => iptr.ImprovementPlanResponseFiles)
                            .Include(ip => ip.ImprovementPlanTasks)
                            .ThenInclude(ipt => ipt.ImprovementPlanTaskFiles)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.Evaluator)
                            .Include(ip => ip.CollaboratorCriteriaAnswer)
                            .ThenInclude(eca => eca.CollaboratorCriteria)
                            .ThenInclude(ec => ec.CollaboratorEvaluated)
                            .ThenInclude(c => c.Position)
                            .Where
                            (
                                ip
                                =>
                                ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluatedId == request.CollaboratorId
                                &&
                                DbFunctions.DbFunctions.RemoveAccents
                                (
                                    ip.CollaboratorCriteriaAnswer.CollaboratorCriteria.CollaboratorEvaluated.Name.ToLower()
                                )
                                .Contains(DbFunctions.DbFunctions.RemoveAccents(request.CollaboratorName.ToLower()))
                                &&
                                ip.ImprovementPlanTasks.All
                                (
                                    ipt
                                    =>
                                    ipt.ImprovementPlanResponse == null
                                    ||
                                    ipt.ImprovementPlanResponse.Count == 0
                                    ||
                                    string.IsNullOrEmpty(ipt.ImprovementPlanResponse[0].TaskResponse)
                                )
                            );
                    }

                    break;
                default:
                    break;
            };

            int totalCount = await baseQuery.CountAsync();
            List<ImprovementPlan> items = request.Page == 0 || request.PageSize == 0
                ? [.. baseQuery]
                : await baseQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

            return new SearchFilter<ImprovementPlan>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
