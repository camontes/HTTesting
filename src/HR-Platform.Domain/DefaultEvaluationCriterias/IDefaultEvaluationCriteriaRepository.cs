namespace HR_Platform.Domain.DefaultEvaluationCriterias;

public interface IDefaultEvaluationCriteriaRepository
{
    Task<List<DefaultEvaluationCriteria>> GetAll();
}

