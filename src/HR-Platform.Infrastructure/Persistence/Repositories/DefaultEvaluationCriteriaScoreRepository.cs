using HR_Platform.Domain.DefaultEvaluationCriteriaScores;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class DefaultEvaluationCriteriaScoreRepository(ApplicationDbContext context) : IDefaultEvaluationCriteriaScoreRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<List<DefaultEvaluationCriteriaScore>> GetAll()
            =>
        await _context.DefaultEvaluationCriteriaScores
            .Where(ec => ec.DefaultEvaluationCriteriaId != 8)
            .OrderBy(ec => ec.Id)
            .AsNoTracking()
            .ToListAsync();

        public async Task<List<DefaultEvaluationCriteriaScore>> GetByDefaultEvaluationCriteriaIdAsync(int defaultEvaluationCriteriaId)
            =>
            await _context.DefaultEvaluationCriteriaScores
            .Where(ec => ec.DefaultEvaluationCriteriaId == defaultEvaluationCriteriaId)
            .OrderBy(ec => ec.Id)
            .AsNoTracking()
            .ToListAsync();
    }
}

