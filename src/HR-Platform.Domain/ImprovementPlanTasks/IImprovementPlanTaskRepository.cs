using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.SearchFilters;

namespace HR_Platform.Domain.ImprovementPlanTasks;

public interface IImprovementPlanTaskRepository
{
    Task<ImprovementPlanTask?> GetByIdAsync(ImprovementPlanTaskId id);
    Task<List<ImprovementPlanTask>?> GetByCollaboratorCriteriaAnswerIdAsync(CollaboratorCriteriaAnswerId CollaboratorCriteriaAnswerId);
    Task<List<ImprovementPlanTask>?> GetByEvaluatorIdAsync(CollaboratorId evaluatorId);
    Task<SearchFilter<ImprovementPlanTask>> GetByEvaluatorIdAndNameAsync(ImprovementPlansRequestSearch request);
    Task<int> CountTasksByCollaboratorAsync(CollaboratorCriteriaAnswerId CollaboratorCriteriaAnswerId);
    Task<bool> ExistsAsync(ImprovementPlanTaskId id);
    void Add(ImprovementPlanTask improvementPlanTask);
    void AddRange(List<ImprovementPlanTask> improvementPlansTask);
    void Update(ImprovementPlanTask improvementPlanTask);
    void Delete(ImprovementPlanTask improvementPlanTask);
}
