using HR_Platform.Domain.InductionFiles;
using HR_Platform.Domain.Inductions;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class InductionFileRepository(ApplicationDbContext context) : IInductionFileRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(InductionFile InductionFile) => _context.InductionFiles.Add(InductionFile);

        public void Delete(InductionFile InductionFile) => _context.InductionFiles.Remove(InductionFile);
        
        public void DeleteRange(List<InductionFile> tags) => _context.InductionFiles.RemoveRange(tags);

        public void Update(InductionFile InductionFile) => _context.InductionFiles.Update(InductionFile);

        public async Task<List<InductionFile>> GetAll() => await _context.InductionFiles
            .AsNoTracking()
            .ToListAsync();

        public async Task<InductionFile?> GetByIdAsync(InductionFileId Id) =>
            await _context.InductionFiles
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<InductionFile>?> GetByInductioIdAsync(InductionId inductionId) =>
                 await _context.InductionFiles.Where(t => t.InductionId == inductionId)
                    .ToListAsync();

        public void AddRangeInductionFiles(List<InductionFile> InductionFiles) => _context.InductionFiles.AddRange(InductionFiles);
        
    }
}
