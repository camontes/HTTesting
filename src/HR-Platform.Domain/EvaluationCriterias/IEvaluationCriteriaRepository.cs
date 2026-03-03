using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.Positions;

namespace HR_Platform.Domain.EvaluationCriterias;

public interface IEvaluationCriteriaRepository
{
    Task<List<EvaluationCriteria>> GetAll();
    Task<EvaluationCriteria?> GetByIdAsync(EvaluationCriteriaId id);
    Task<List<EvaluationCriteria>?> GetByPositionIdAsync(PositionId positionId);
    Task<List<EvaluationCriteria>?> GetByPositionIdWithChildrenAsync(PositionId positionId);
    Task<List<EvaluationCriteria>?> GetByPositionIdAndEvaluationCriteriaTypeIdAsync(PositionId positionId, EvaluationCriteriaTypeId evaluationCriteriaTypeId);
    Task<List<EvaluationCriteria>?> GetByPositionIdAndEvaluationCriteriaTypeIdForUpdatingAsync(PositionId positionId, EvaluationCriteriaTypeId evaluationCriteriaTypeId);
    Task<bool> ExistsAsync(EvaluationCriteriaId id);
    void AddRangeEvaluationCriterias(List<EvaluationCriteria> EvaluationCriteria);
    void Add(EvaluationCriteria pension);
    void Update(EvaluationCriteria EvaluationCriteria);
    void UpdateRange(List<EvaluationCriteria> EvaluationCriterias);
    void Delete(EvaluationCriteria EvaluationCriteria);
    void DeleteRange(List<EvaluationCriteria> EvaluationCriterias);
}
