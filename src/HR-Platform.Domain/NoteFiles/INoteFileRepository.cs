using HR_Platform.Domain.Notes;

namespace HR_Platform.Domain.NoteFiles;

public interface INoteFileRepository
{
    Task<List<NoteFile>> GetAll();
    Task<NoteFile?> GetByIdAsync(NoteFileId id);
    Task<List<NoteFile>?> GetByInductioIdAsync(NoteId noteId);
    void AddRangeNoteFiles(List<NoteFile> NoteFile);
    void Add(NoteFile NoteFile);
    void Update(NoteFile NoteFile);
    void Delete(NoteFile NoteFile);
    void DeleteRange(List<NoteFile> tags);
}
