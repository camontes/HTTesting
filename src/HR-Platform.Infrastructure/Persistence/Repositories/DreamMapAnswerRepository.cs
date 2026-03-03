using HR_Platform.Domain.CollaboratorDreamMapAnswers;
using HR_Platform.Domain.DreamMapAnswers;
using HR_Platform.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class DreamMapAnswerRepository(ApplicationDbContext context) : IDreamMapAnswerRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(DreamMapAnswer DreamMapAnswer) => _context.DreamMapAnswers.Add(DreamMapAnswer);

        public void Delete(DreamMapAnswer DreamMapAnswer) => _context.DreamMapAnswers.Remove(DreamMapAnswer);
        public void DeleteRange(List<DreamMapAnswer> DreamMapAnswer) => _context.DreamMapAnswers.RemoveRange(DreamMapAnswer);
        public void Update(DreamMapAnswer DreamMapAnswer) => _context.DreamMapAnswers.Update(DreamMapAnswer);
        public void UpdateRange(List<DreamMapAnswer> DreamMapAnswers) => _context.DreamMapAnswers.UpdateRange(DreamMapAnswers);

        public async Task<List<DreamMapAnswer>> GetAll() => await _context.DreamMapAnswers
            .AsNoTracking()
            .ToListAsync();

        public async Task<List<DreamMapAnswer>> GetAllCollaborators() => await _context.DreamMapAnswers
            .AsNoTracking()
            .ToListAsync();

        public async Task<DreamMapAnswer?> GetByIdAsync(DreamMapAnswerId Id) =>
            await _context.DreamMapAnswers
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public void AddRangeDreamMapAnswers(List<DreamMapAnswer> DreamMapAnswer) => _context.DreamMapAnswers
            .AddRange(DreamMapAnswer);

        public async Task<List<DreamMapAnswer>> GetAllCollaboratorsAnswers(CollaboratorDreamMapAnswerId collaboratorDreamMapAnswerId) =>
            await _context.DreamMapAnswers
            .Where(x => x.CollaboratorDreamMapAnswerId == collaboratorDreamMapAnswerId)
            .ToListAsync();
    }
}
