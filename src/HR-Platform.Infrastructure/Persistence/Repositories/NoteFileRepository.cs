using HR_Platform.Domain.NoteFiles;
using HR_Platform.Domain.Notes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class NoteFileRepository(ApplicationDbContext context) : INoteFileRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(NoteFile NoteFile) => _context.NoteFiles.Add(NoteFile);

        public void Delete(NoteFile NoteFile) => _context.NoteFiles.Remove(NoteFile);
        
        public void DeleteRange(List<NoteFile> tags) => _context.NoteFiles.RemoveRange(tags);

        public void Update(NoteFile NoteFile) => _context.NoteFiles.Update(NoteFile);

        public async Task<List<NoteFile>> GetAll() => await _context.NoteFiles
            .AsNoTracking()
            .ToListAsync();

        public async Task<NoteFile?> GetByIdAsync(NoteFileId Id) =>
            await _context.NoteFiles
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<NoteFile>?> GetByInductioIdAsync(NoteId noteId) =>
                 await _context.NoteFiles.Where(t => t.NoteId == noteId)
                    .ToListAsync();

        public void AddRangeNoteFiles(List<NoteFile> NoteFiles) => _context.NoteFiles.AddRange(NoteFiles);
        
    }
}
