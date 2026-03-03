using HR_Platform.Domain.NoteViewers;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class NoteViewerRepository(ApplicationDbContext context) : INoteViewerRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(NoteViewer NoteViewer) => _context.NoteViewers.Add(NoteViewer);
        public void AddRange(List<NoteViewer> NoteViewers) => _context.NoteViewers.AddRange(NoteViewers);

        public void Delete(NoteViewer NoteViewer) => _context.NoteViewers.Remove(NoteViewer);
        public void Update(NoteViewer NoteViewer) => _context.NoteViewers.Update(NoteViewer);

        public async Task<bool> ExistsAsync(NoteViewerId id) => await _context.NoteViewers
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<NoteViewer?> GetByIdAsync(NoteViewerId Id) =>
            await _context.NoteViewers
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

    
    }
}
