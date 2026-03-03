using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.ImprovementPlans;

public interface IImprovementPlanRepository
{
    Task<ImprovementPlan?> GetByIdAsync(ImprovementPlanId id);
    Task<SearchFilter<ImprovementPlan>> GetByEvaluatorIdAndNameAsync(ImprovementPlansRequestSearch request);
    Task<SearchFilter<ImprovementPlan>> GetByCollaboratorIdAndNameAsync(ImprovementPlansRequestSearch request);
    void Add(ImprovementPlan improvementPlan);
}
