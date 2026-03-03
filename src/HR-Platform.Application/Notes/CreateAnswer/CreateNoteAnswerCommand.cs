using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Notes.CreateAnswer;

public record CreateNoteAnswerCommand(
    string EmailChangeBy,
    string Description,
    Guid MainNoteId,
    List<Guid>? Viewers,
    List<NoteAnswersFileObject> NotesList
) : IRequest<ErrorOr<bool>>;

public record NoteAnswersFileObject(
    IFormFile NotesFile,
    string FileName,
    string UrlFile
);

