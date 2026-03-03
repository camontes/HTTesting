using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.BrigadeInventoryFiles;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class BrigadeInventoryFileRepository(ApplicationDbContext context) : IBrigadeInventoryFileRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(BrigadeInventoryFile BrigadeInventoryFile) => _context.BrigadeInventoryFiles.Add(BrigadeInventoryFile);

        public void Delete(BrigadeInventoryFile BrigadeInventoryFile) => _context.BrigadeInventoryFiles.Remove(BrigadeInventoryFile);
        
        public void DeleteRange(List<BrigadeInventoryFile> tags) => _context.BrigadeInventoryFiles.RemoveRange(tags);

        public void Update(BrigadeInventoryFile BrigadeInventoryFile) => _context.BrigadeInventoryFiles.Update(BrigadeInventoryFile);

        public async Task<List<BrigadeInventoryFile>> GetAll() => 
            await _context.BrigadeInventoryFiles
            .AsNoTracking()
            .ToListAsync();

        public async Task<BrigadeInventoryFile?> GetByIdAsync(BrigadeInventoryFileId Id) =>
            await _context.BrigadeInventoryFiles
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<BrigadeInventoryFile>?> GetByBrigadeInventoryIdAsync(BrigadeInventoryId inductionId) =>
                 await _context.BrigadeInventoryFiles
                    .Where(t => t.BrigadeInventoryId == inductionId)
                    .ToListAsync();

        public void AddRangeBrigadeInventoryFiles(List<BrigadeInventoryFile> BrigadeInventoryFiles) => 
            _context.BrigadeInventoryFiles
            .AddRange(BrigadeInventoryFiles);
        
    }
}
