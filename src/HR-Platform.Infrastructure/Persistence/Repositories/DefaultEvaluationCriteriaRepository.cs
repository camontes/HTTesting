using HR_Platform.Domain.DefaultEvaluationCriterias;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class DefaultEvaluationCriteriaRepository(ApplicationDbContext context) : IDefaultEvaluationCriteriaRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<List<DefaultEvaluationCriteria>> GetAll()
            =>
            await _context.DefaultEvaluationCriterias
            .Where(ec => ec.Id != new DefaultEvaluationCriteriaId(8))
            .OrderBy(ec => ec.Id)
            .AsNoTracking()
            .ToListAsync();
    }
}
