using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR_Platform.Application.Notes.Create;

public record CreateNoteCommand(
    string EmailChangeBy,
    string Description,
    Guid CollaboratorId,
    bool IsPublic,
    List<CreateNotesObjectFile> NotesList
) : IRequest<ErrorOr<bool>>;

public record CreateNotesObjectFile(
    IFormFile NoteFile,
    string FileName,
    string UrlFile
);

