using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.EvaluationCriteriaScores;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class EvaluationCriteriaScoreRepository(ApplicationDbContext context) : IEvaluationCriteriaScoreRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(EvaluationCriteriaScore evaluationCriteriaScore) => _context.EvaluationCriteriaScores.Add(evaluationCriteriaScore);

        public void Update(EvaluationCriteriaScore evaluationCriteriaScore) => _context.EvaluationCriteriaScores.Update(evaluationCriteriaScore);
    }
}
