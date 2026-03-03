using HR_Platform.Domain.ImprovementPlanTaskFiles;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class ImprovementPlanTaskFileRepository(ApplicationDbContext context) : IImprovementPlanTaskFileRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(ImprovementPlanTaskFile ImprovementPlanFile) => _context.ImprovementPlanTaskFiles.Add(ImprovementPlanFile);
        public void AddRange(List<ImprovementPlanTaskFile> ImprovementPlanFiles) => _context.ImprovementPlanTaskFiles.AddRange(ImprovementPlanFiles);

        public void Delete(ImprovementPlanTaskFile ImprovementPlanFile) => _context.ImprovementPlanTaskFiles.Remove(ImprovementPlanFile);
        public void Update(ImprovementPlanTaskFile ImprovementPlanFile) => _context.ImprovementPlanTaskFiles.Update(ImprovementPlanFile);

        public async Task<bool> ExistsAsync(ImprovementPlanTaskFileId id) => await _context.ImprovementPlanTaskFiles
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<ImprovementPlanTaskFile?> GetByIdAsync(ImprovementPlanTaskFileId Id) =>
            await _context.ImprovementPlanTaskFiles
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);
    }
}
