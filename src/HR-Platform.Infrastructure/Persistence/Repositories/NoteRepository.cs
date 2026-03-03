using HR_Platform.Domain.Collaborators;
using HR_Platform.Domain.Notes;
using Microsoft.EntityFrameworkCore;

namespace HR_Platform.Infrastructure.Persistence.Repositories
{
    public class NoteRepository(ApplicationDbContext context) : INoteRepository
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(Note Note) => _context.Notes.Add(Note);
        public void AddRange(List<Note> Notes) => _context.Notes.AddRange(Notes);

        public void Delete(Note Note) => _context.Notes.Remove(Note);
        public void Update(Note Note) => _context.Notes.Update(Note);

        public async Task<bool> ExistsAsync(NoteId id) => await _context.Notes
            .AsNoTracking()
            .AnyAsync(r => r.Id == id);

        public async Task<Note?> GetByIdAsync(NoteId Id) =>
            await _context.Notes
            .Include(x => x.Creator)
            .Include(x => x.Assignee)
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == Id);

        public async Task<List<Note>> GetByCollaboratorIdAsync(CollaboratorId collaboratorId) =>
            await _context.Notes
            .Include(x=> x.Creator)
            .Include(x=> x.Assignee)
            .Include(x=> x.ParentNote)
            .Include(x=> x.NoteFiles)
            .Where(x => x.AssignedTo == collaboratorId)
            .ToListAsync();
        
    }
}
