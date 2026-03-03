using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.Positions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class EvaluationCriteriaRepository(ApplicationDbContext context) : IEvaluationCriteriaRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(EvaluationCriteria EvaluationCriteria) => _context.EvaluationCriterias.Add(EvaluationCriteria);

        public void Delete(EvaluationCriteria EvaluationCriteria) => _context.EvaluationCriterias.Remove(EvaluationCriteria);
        public void DeleteRange(List<EvaluationCriteria> EvaluationCriterias) => _context.EvaluationCriterias.RemoveRange(EvaluationCriterias);
        public void Update(EvaluationCriteria EvaluationCriteria) => _context.EvaluationCriterias.Update(EvaluationCriteria);
        public void UpdateRange(List<EvaluationCriteria> EvaluationCriterias) => _context.EvaluationCriterias.UpdateRange(EvaluationCriterias);

        public async Task<bool> ExistsAsync(EvaluationCriteriaId id) => await _context.EvaluationCriterias
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<List<EvaluationCriteria>> GetAll() => await _context.EvaluationCriterias
            .AsNoTracking()
            .ToListAsync();

        public async Task<EvaluationCriteria?> GetByIdAsync(EvaluationCriteriaId Id) =>
            await _context.EvaluationCriterias
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public void AddRangeEvaluationCriterias(List<EvaluationCriteria> EvaluationCriterias) => _context.EvaluationCriterias
            .AddRange(EvaluationCriterias);

        public async Task<List<EvaluationCriteria>?> GetByPositionIdAsync(PositionId positionId) => await _context.EvaluationCriterias
            .Where(r => r.PositionId == positionId)
            .AsNoTracking()
            .ToListAsync();

        public async Task<List<EvaluationCriteria>?> GetByPositionIdWithChildrenAsync(PositionId positionId) => await _context.EvaluationCriterias
            .Where(r => r.PositionId == positionId)
            .Include(ec => ec.EvaluationCriteriaScores)
            .AsNoTracking()
            .ToListAsync();

        public async Task<List<EvaluationCriteria>?> GetByPositionIdAndEvaluationCriteriaTypeIdAsync(PositionId positionId, EvaluationCriteriaTypeId evaluationCriteriaTypeId)
            =>
            await _context.EvaluationCriterias
            .Where(ec => ec.PositionId == positionId && ec.EvaluationCriteriaTypeId == evaluationCriteriaTypeId)
            .Include(ec => ec.EvaluationCriteriaScores)
            .AsNoTracking()
            .ToListAsync();

        public async Task<List<EvaluationCriteria>?> GetByPositionIdAndEvaluationCriteriaTypeIdForUpdatingAsync(PositionId positionId, EvaluationCriteriaTypeId evaluationCriteriaTypeId)
             =>
             await _context.EvaluationCriterias
             .Where(ec => ec.PositionId == positionId && ec.EvaluationCriteriaTypeId == evaluationCriteriaTypeId)
             .ToListAsync();

    }
}
