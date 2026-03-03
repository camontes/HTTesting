using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.CollaboratorBrigadeInventoryFiles;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class CollaboratorBrigadeInventoryFileRepository(ApplicationDbContext context) : ICollaboratorBrigadeInventoryFileRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(CollaboratorBrigadeInventoryFile CollaboratorBrigadeInventoryFile) => _context.CollaboratorBrigadeInventoryFile.Add(CollaboratorBrigadeInventoryFile);

        public void Delete(CollaboratorBrigadeInventoryFile CollaboratorBrigadeInventoryFile) => _context.CollaboratorBrigadeInventoryFile.Remove(CollaboratorBrigadeInventoryFile);
        
        public void DeleteRange(List<CollaboratorBrigadeInventoryFile> tags) => _context.CollaboratorBrigadeInventoryFile.RemoveRange(tags);

        public void Update(CollaboratorBrigadeInventoryFile CollaboratorBrigadeInventoryFile) => _context.CollaboratorBrigadeInventoryFile.Update(CollaboratorBrigadeInventoryFile);

        public async Task<List<CollaboratorBrigadeInventoryFile>> GetAll() => 
            await _context.CollaboratorBrigadeInventoryFile
            .AsNoTracking()
            .ToListAsync();

        public async Task<CollaboratorBrigadeInventoryFile?> GetByIdAsync(CollaboratorBrigadeInventoryFileId Id) =>
            await _context.CollaboratorBrigadeInventoryFile
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<CollaboratorBrigadeInventoryFile>?> GetByCollaboratorBrigadeInventoryIdAsync(CollaboratorBrigadeInventoryId inductionId) =>
                 await _context.CollaboratorBrigadeInventoryFile
                    .Where(t => t.CollaboratorBrigadeInventoryId == inductionId)
                    .ToListAsync();

        public void AddRangeCollaboratorBrigadeInventoryFiles(List<CollaboratorBrigadeInventoryFile> CollaboratorBrigadeInventoryFile) => 
            _context.CollaboratorBrigadeInventoryFile
            .AddRange(CollaboratorBrigadeInventoryFile);

    }
}
