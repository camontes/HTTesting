namespace HR_Platform.Domain.DefaultEvaluationCriteriaScores;

public interface IDefaultEvaluationCriteriaScoreRepository
{
    Task<List<DefaultEvaluationCriteriaScore>> GetAll();
    Task<List<DefaultEvaluationCriteriaScore>> GetByDefaultEvaluationCriteriaIdAsync(int defaultEvaluationCriteriaId);
}

