using HR_Platform.Domain.Collaborators;

namespace HR_Platform.Domain.Notes;

public interface INoteRepository
{
    Task<Note?> GetByIdAsync(NoteId id);
    Task<List<Note>> GetByCollaboratorIdAsync(CollaboratorId collaboratorid);
    Task<bool> ExistsAsync(NoteId id);
    void Add(Note pension);
    void AddRange(List<Note> Notes);
    void Update(Note Note);
    void Delete(Note Note);
}
