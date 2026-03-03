namespace HR_Platform.Domain.EvaluationCriteriaScores;

public interface IEvaluationCriteriaScoreRepository
{
    void Add(EvaluationCriteriaScore evaluationCriteriaScore);
    void Update(EvaluationCriteriaScore evaluationCriteriaScore);
}

