using HR_Platform.Domain.Companies;

namespace HR_Platform.Domain.NoteViewers;

public interface INoteViewerRepository
{
    Task<NoteViewer?> GetByIdAsync(NoteViewerId id);
    Task<bool> ExistsAsync(NoteViewerId id);
    void Add(NoteViewer pension);
    void AddRange(List<NoteViewer> NoteViewers);
    void Update(NoteViewer NoteViewer);
    void Delete(NoteViewer NoteViewer);
}
